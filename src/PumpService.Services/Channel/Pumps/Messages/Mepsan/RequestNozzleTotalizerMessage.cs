namespace PumpService.Services.Channel.Pumps.Messages.Mepsan
{
    internal class RequestNozzleTotalizerMessage : IMessage  //CD101
    {
        #region Fields

        public byte ProtocolDataUnitData { get; set; }

        #endregion Fields

        #region Constructor

        public RequestNozzleTotalizerMessage(byte pPompaAdres, byte pNozzleNumber)
        {
            SlaveAddress = pPompaAdres;
            ProtocolDataUnitData = pNozzleNumber;
        }

        #endregion Constructor

        #region Methods

        public void Initialize(byte[] frame)
        {
        }

        #endregion Methods

        #region Properties

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

        public byte[] ProtocolDataUnit
        {
            get
            {
                Byte[] data = new Byte[3];

                data[0] = 0x65; // Transaction CD101

                data[1] = (byte)1; // Bu Bytten sonra gelen byte sayısı, length

                data[2] = ProtocolDataUnitData;//nozzle number

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

        #endregion Properties
    }
}