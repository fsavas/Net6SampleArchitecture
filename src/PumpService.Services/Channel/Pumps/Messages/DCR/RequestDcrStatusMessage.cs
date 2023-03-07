namespace PumpService.Services.Channel.Pumps.Messages.DCR
{
    internal class RequestDcrStatusMessage : IMessage  // CD148
    {
        public RequestDcrStatusMessage(byte _address, byte[] _ProtocolDataUnitData)
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

        public byte[] ProtocolDataUnit
        {
            get
            {
                Byte[] data = new Byte[ProtocolDataUnitData.Length + 2];

                data[0] = 0x94; // TransactionId Yani Komut Kodu

                data[1] = (byte)2; // Bu Bytten sonra gelen byte sayısı

                for (int i = 0; i < ProtocolDataUnitData.Length; i++)
                {
                    data[2 + i] = ProtocolDataUnitData[i];
                }

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