// See https://aka.ms/new-console-template for more information

using Chaos.NaCl;
using Newtonsoft.Json;
using Plutonication;
using SamplePlutonicationWalletCSharp;
using Schnorrkel;
using Substrate.NetApi;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Rpc;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;

Account account = Helpers.GetAccount();
Console.WriteLine("Public key: " + account.Value);

SubstrateClient substrateClient = new SubstrateClient(
    new Uri("wss://ws.test.azero.dev"),
    Substrate.NetApi.Model.Extrinsics.ChargeTransactionPayment.Default());

await substrateClient.ConnectAsync();

Console.WriteLine(String.Format("Balance: {0:0.00}",
    await Helpers.GetAccountBalance(substrateClient, account.Value)));

await PlutonicationWalletClient.InitializeAsync(
    ac: new AccessCredentials(new Uri("plutonication:?key=1&url=wss://plutonication-acnha.ondigitalocean.app/")),
    pubkey: account.Value,
    signPayload: payloadJson =>
    {
        try
        {
            Console.WriteLine("Payload: ");
            Console.WriteLine(payloadJson);

            Plutonication.Payload payload = JsonConvert.DeserializeObject<Plutonication.Payload[]>(payloadJson.ToString())[0];

            Console.WriteLine("Payload received, nice!");
            Console.WriteLine(payload.method);

            byte[] methodBytes = Utils.HexToByteArray(payload.method);

            List<byte> methodParameters = new List<byte>();

            for (int i = 2; i < methodBytes.Length; i++)
            {
                methodParameters.Add(methodBytes[i]);
            }

            Method method = new Method(methodBytes[0], methodBytes[1], methodParameters.ToArray());

            Hash eraHash = new Hash();
            eraHash.Create(Utils.HexToByteArray(payload.era));

            Hash blockHash = new Hash();
            blockHash.Create(payload.blockHash);

            Console.WriteLine("HexEra: " + payload.era);
            Console.WriteLine(eraHash);

            Hash genesisHash = new Hash();
            genesisHash.Create(Utils.HexToByteArray(payload.genesisHash));

            RuntimeVersion runtime = new RuntimeVersion
            {
                ImplVersion = payload.version,
                SpecVersion = Helpers.HexStringToUint(payload.specVersion),
                TransactionVersion = Helpers.HexStringToUint(payload.transactionVersion),
            };

            var extrinsic = RequestGenerator.SubmitExtrinsic(
                true,
                account,
                method,
                Era.Decode(Utils.HexToByteArray(payload.era)),
                Helpers.HexStringToUint(payload.nonce),
                ChargeAssetTxPayment.Default(),
                genesisHash,
                blockHash,
                runtime
            );

            var signerResult = new SignerResult
            {
                id = 1,
                signature = Utils.Bytes2HexString(extrinsic.Signature).ToLower(),
            };

            Console.WriteLine("Signature: " + Utils.Bytes2HexString(extrinsic.Signature).ToLower());

            Console.WriteLine("Nearly done");

            
            PlutonicationWalletClient.SendPayloadSignatureAsync(signerResult);
            Console.WriteLine("Signature is on the way...");
        }
        catch (Exception ex)
        {
            Console.WriteLine("PlutonicationWallet error");
            Console.WriteLine(ex.Message);

        }
    },
    signRaw: raw =>
    {
        Plutonication.Message message = JsonConvert.DeserializeObject<Plutonication.Message[]>(raw.ToString())[0];

        if (message.type != "bytes")
        {
            throw new Exception("Message is not in bytes format");
        }
        byte[] msg = Utils.HexToByteArray(message.data);

        if (msg.Length > 256) msg = HashExtension.Blake2(msg, 256);

        byte[] signature;
        switch (account.KeyType)
        {
            case KeyType.Ed25519:
                signature = Ed25519.Sign(msg, account.PrivateKey);
                break;

            case KeyType.Sr25519:
                signature = Sr25519v091.SignSimple(account.Bytes, account.PrivateKey, msg);
                break;

            default:
                throw new Exception($"Unknown key type found '{account.KeyType}'.");
        }

        var signerResult = new SignerResult
        {
            id = 1,
            signature = Utils.Bytes2HexString(signature).ToLower(),
        };

        PlutonicationWalletClient.SendRawSignatureAsync(signerResult);
        Console.WriteLine("Signature is on the way...");
    });

Console.ReadKey();

Console.WriteLine("Bye bye.");