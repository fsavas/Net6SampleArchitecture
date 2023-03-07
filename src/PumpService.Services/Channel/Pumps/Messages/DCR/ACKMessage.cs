namespace PumpService.Services.Channel.Pumps.Messages.DCR
{
    public class ACKMessage : IMessage
    {
        public ACKMessage(byte _slaveAddress)
        {
            SlaveAddress = _slaveAddress;
        }

        public byte SlaveAddress { get; set; }

        public byte[] MessageFrame
        {
            get;
            set;
        }

        public byte[] ProtocolDataUnit
        {
            get
            {
                byte[] data = new byte[] { SlaveAddress, 0x0c, 0xFA };
                return null;
            }
        }

        public ushort TransactionId
        {
            get { return StateEngine.SlaveTxNo; }
            set { int x = value; }
        }

        public void Initialize(byte[] frame)
        {
        }

        public byte ControlNo
        {
            get
            {
                return 0xC0; // ACK
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}