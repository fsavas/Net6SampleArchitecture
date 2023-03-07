namespace PumpService.Services.Channel.Pumps.Messages.DCR
{
    internal class UpdateUnitPriceMessage : IMessage
    {
        public UpdateUnitPriceMessage(byte _address, byte[] _ProtocolDataUnitData)
        {
            SlaveAddress = _address;
            ProtocolDataUnitData = _ProtocolDataUnitData;
        }

        public byte SlaveAddress
        {
            get;
            set;
        }

        public byte[] MessageFrame
        {
            get;
            set;
        }

        public byte ControlNo
        {
            get
            {
                return 0x30;//data mesajı olduğunu gösteriyor
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public byte[] ProtocolDataUnitData { get; set; }

        public byte[] ProtocolDataUnit //CD140 implementation
        {
            get
            {
                Byte[] data = new Byte[ProtocolDataUnitData.Length + 2];

                data[0] = 0x8C; // TransactionId Yani Komut Kodu

                data[1] = (byte)ProtocolDataUnitData.Length; // Bu Bytten sonra gelen byte sayısı

                for (int i = 0; i < ProtocolDataUnitData.Length; i++)
                {
                    data[2 + i] = ProtocolDataUnitData[i];
                }

                //data[2 + ProtocolDataUnitData.Length] = 18;
                //data[2 + ProtocolDataUnitData.Length] = 0;
                //data[2 + ProtocolDataUnitData.Length] = 0;
                //data[2 + ProtocolDataUnitData.Length] = 0;

                //data[2 + ProtocolDataUnitData.Length] = 19;
                //data[2 + ProtocolDataUnitData.Length] = 0;
                //data[2 + ProtocolDataUnitData.Length] = 0;
                //data[2 + ProtocolDataUnitData.Length] = 0;

                return data;
            }
        }

        public ushort TransactionId
        {
            get
            {
                return StateEngine.SlaveTxNo;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Initialize(byte[] frame)
        {
        }
    }
}