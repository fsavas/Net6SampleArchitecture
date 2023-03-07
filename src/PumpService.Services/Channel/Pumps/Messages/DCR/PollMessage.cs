namespace PumpService.Services.Channel.Pumps.Messages.DCR
{
    public class PollMessage : IMessage
    {
        public PollMessage()
        {
            SlaveAddress = 1;
        }

        public PollMessage(byte _address)
        {
            SlaveAddress = _address;
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

        public byte[] ProtocolDataUnit
        {
            get
            {
                byte[] data = new byte[] { SlaveAddress, 0x20, 0xFA };

                return null;
            }
        }

        private int txNo;

        public ushort TransactionId
        {
            get { return 0; }
            set { txNo = value; }
        }

        public void Initialize(byte[] frame)
        {
            MessageFrame = new Byte[frame.Length];
            frame.CopyTo(MessageFrame, 0);
        }

        private byte _ctrlNo;

        public byte ControlNo
        {
            get { return 0x20; }
            set { _ctrlNo = value; }
        }
    }
}