using System;
using System.Globalization;
using System.Net;
using Schnorrkel.Keys;
using Substrate.NetApi;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;

namespace SamplePlutonicationWalletCSharp
{
	public class Helpers
	{
        private const string PRIVATE_KEY = "0x34847CDDAA7E3E253D6DC7B7BB1D819B6469B8256C28F46E8CC6D07DA91B240AD3B76034856F5DCB0DF96194ABFD0AF1E1202554C99565E5C65F86CB69BC6D9D";

        public Helpers()
		{
		}

		public static Account GetAccount()
		{
            var miniSecret = new MiniSecret(Utils.HexToByteArray(PRIVATE_KEY), ExpandMode.Ed25519);

            return Account.Build(KeyType.Sr25519,
                miniSecret.ExpandToSecret().ToBytes(),
                miniSecret.GetPair().Public.Key);
        }

        public static uint HexStringToUint(string hex)
        {
            hex = hex.Replace("0x", ""); // remove the 0x if it's there
            if (uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint result))
            {
                return result;
            }
            else
            {
                throw new FormatException("The provided string is not a valid hexadecimal number");
            }
        }


        public static async Task<double> GetAccountBalance(SubstrateClient client, string substrateAddress)
        {
            
            var account32 = new AccountId32();
            account32.Create(Utils.GetPublicKeyFrom(substrateAddress));

            string parameters = AccountParams(account32);
            var accountInfo = await client.GetStorageAsync<AccountInfo>(parameters, CancellationToken.None);

            int decimals = 12;

            return (double)accountInfo.Data.Free.Value / Math.Pow(10, decimals);
        }

        /// <summary>
        /// >> AccountParams
        ///  The full account information for a particular account ID.
        /// </summary>
        private static string AccountParams(AccountId32 key)
        {
            return RequestGenerator.GetStorage("System", "Account", Substrate.NetApi.Model.Meta.Storage.Type.Map, new Substrate.NetApi.Model.Meta.Storage.Hasher[] {
                        Substrate.NetApi.Model.Meta.Storage.Hasher.BlakeTwo128Concat}, new Substrate.NetApi.Model.Types.IType[] {
                        key});
        }
    }
}

