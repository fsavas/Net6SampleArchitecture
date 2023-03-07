namespace PumpService.Services.Channel.Pumps.Messages.Mepsan
{
    public class AllowedNozzleNumberMessage : IMessage
    {
        #region Fields

        private byte _ctrlNo;
        public byte ProtocolDataUnitData { get; set; }

        #endregion Fields

        #region Constructor

        public AllowedNozzleNumberMessage(byte pPompaAdres, byte pNozzleNumber)
        {
            SlaveAddress = pPompaAdres;
            ProtocolDataUnitData = pNozzleNumber;
        }

        #endregion Constructor

        #region Methods

        public void Initialize(byte[] frame)
        {
            //MessageFrame = new Byte[frame.Length];
            //frame.CopyTo(MessageFrame, 0);
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

                data[0] = 0x02; // Transaction CD2

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