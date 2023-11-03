using System;
using Substrate.NetApi.Attributes;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Metadata.V14;

namespace SamplePlutonicationWalletCSharp
{
    /// <summary>
    /// >> 0 - Composite[sp_core.crypto.AccountId32]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class AccountId32 : BaseType
    {

        /// <summary>
        /// >> value
        /// </summary>
        private Arr32U8 _value;

        public Arr32U8 Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        public override string TypeName()
        {
            return "AccountId32";
        }

        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Value.Encode());
            return result.ToArray();
        }

        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Value = new Arr32U8();
            Value.Decode(byteArray, ref p);
            TypeSize = p - start;
        }
    }
    /// <summary>
    /// >> 1 - Array
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Array)]
    public sealed class Arr32U8 : BaseType
    {

        private Substrate.NetApi.Model.Types.Primitive.U8[] _value;

        public override int TypeSize
        {
            get
            {
                return 32;
            }
        }

        public Substrate.NetApi.Model.Types.Primitive.U8[] Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        public override string TypeName()
        {
            return string.Format("[{0}; {1}]", new Substrate.NetApi.Model.Types.Primitive.U8().TypeName(), this.TypeSize);
        }

        public override byte[] Encode()
        {
            var result = new List<byte>();
            foreach (var v in Value) { result.AddRange(v.Encode()); };
            return result.ToArray();
        }

        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            var array = new Substrate.NetApi.Model.Types.Primitive.U8[TypeSize];
            for (var i = 0; i < array.Length; i++) { var t = new Substrate.NetApi.Model.Types.Primitive.U8(); t.Decode(byteArray, ref p); array[i] = t; };
            var bytesLength = p - start;
            Bytes = new byte[bytesLength];
            System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
            Value = array;
        }

        public void Create(Substrate.NetApi.Model.Types.Primitive.U8[] array)
        {
            Value = array;
            Bytes = Encode();
        }
    }

    /// <summary>
    /// >> 3 - Composite[frame_system.AccountInfo]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class AccountInfo : BaseType
    {

        /// <summary>
        /// >> nonce
        /// </summary>
        private Substrate.NetApi.Model.Types.Primitive.U32 _nonce;

        /// <summary>
        /// >> consumers
        /// </summary>
        private Substrate.NetApi.Model.Types.Primitive.U32 _consumers;

        /// <summary>
        /// >> providers
        /// </summary>
        private Substrate.NetApi.Model.Types.Primitive.U32 _providers;

        /// <summary>
        /// >> sufficients
        /// </summary>
        private Substrate.NetApi.Model.Types.Primitive.U32 _sufficients;

        /// <summary>
        /// >> data
        /// </summary>
        private AccountData _data;

        public Substrate.NetApi.Model.Types.Primitive.U32 Nonce
        {
            get
            {
                return this._nonce;
            }
            set
            {
                this._nonce = value;
            }
        }

        public Substrate.NetApi.Model.Types.Primitive.U32 Consumers
        {
            get
            {
                return this._consumers;
            }
            set
            {
                this._consumers = value;
            }
        }

        public Substrate.NetApi.Model.Types.Primitive.U32 Providers
        {
            get
            {
                return this._providers;
            }
            set
            {
                this._providers = value;
            }
        }

        public Substrate.NetApi.Model.Types.Primitive.U32 Sufficients
        {
            get
            {
                return this._sufficients;
            }
            set
            {
                this._sufficients = value;
            }
        }

        public AccountData Data
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
            }
        }

        public override string TypeName()
        {
            return "AccountInfo";
        }

        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Nonce.Encode());
            result.AddRange(Consumers.Encode());
            result.AddRange(Providers.Encode());
            result.AddRange(Sufficients.Encode());
            result.AddRange(Data.Encode());
            return result.ToArray();
        }

        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Nonce = new Substrate.NetApi.Model.Types.Primitive.U32();
            Nonce.Decode(byteArray, ref p);
            Consumers = new Substrate.NetApi.Model.Types.Primitive.U32();
            Consumers.Decode(byteArray, ref p);
            Providers = new Substrate.NetApi.Model.Types.Primitive.U32();
            Providers.Decode(byteArray, ref p);
            Sufficients = new Substrate.NetApi.Model.Types.Primitive.U32();
            Sufficients.Decode(byteArray, ref p);
            Data = new AccountData();
            Data.Decode(byteArray, ref p);
            TypeSize = p - start;
        }
    }

    /// <summary>
    /// >> 5 - Composite[pallet_balances.AccountData]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class AccountData : BaseType
    {

        /// <summary>
        /// >> free
        /// </summary>
        private Substrate.NetApi.Model.Types.Primitive.U128 _free;

        /// <summary>
        /// >> reserved
        /// </summary>
        private Substrate.NetApi.Model.Types.Primitive.U128 _reserved;

        /// <summary>
        /// >> misc_frozen
        /// </summary>
        private Substrate.NetApi.Model.Types.Primitive.U128 _miscFrozen;

        /// <summary>
        /// >> fee_frozen
        /// </summary>
        private Substrate.NetApi.Model.Types.Primitive.U128 _feeFrozen;

        public Substrate.NetApi.Model.Types.Primitive.U128 Free
        {
            get
            {
                return this._free;
            }
            set
            {
                this._free = value;
            }
        }

        public Substrate.NetApi.Model.Types.Primitive.U128 Reserved
        {
            get
            {
                return this._reserved;
            }
            set
            {
                this._reserved = value;
            }
        }

        public Substrate.NetApi.Model.Types.Primitive.U128 MiscFrozen
        {
            get
            {
                return this._miscFrozen;
            }
            set
            {
                this._miscFrozen = value;
            }
        }

        public Substrate.NetApi.Model.Types.Primitive.U128 FeeFrozen
        {
            get
            {
                return this._feeFrozen;
            }
            set
            {
                this._feeFrozen = value;
            }
        }

        public override string TypeName()
        {
            return "AccountData";
        }

        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Free.Encode());
            result.AddRange(Reserved.Encode());
            result.AddRange(MiscFrozen.Encode());
            result.AddRange(FeeFrozen.Encode());
            return result.ToArray();
        }

        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Free = new Substrate.NetApi.Model.Types.Primitive.U128();
            Free.Decode(byteArray, ref p);
            Reserved = new Substrate.NetApi.Model.Types.Primitive.U128();
            Reserved.Decode(byteArray, ref p);
            MiscFrozen = new Substrate.NetApi.Model.Types.Primitive.U128();
            MiscFrozen.Decode(byteArray, ref p);
            FeeFrozen = new Substrate.NetApi.Model.Types.Primitive.U128();
            FeeFrozen.Decode(byteArray, ref p);
            TypeSize = p - start;
        }
    }
}

