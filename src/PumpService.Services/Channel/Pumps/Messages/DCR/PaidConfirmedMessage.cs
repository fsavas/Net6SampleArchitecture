namespace PumpService.Services.Channel.Pumps.Messages.DCR
{
    public class PaidConfirmedMessage : IMessage
    {
        public PaidConfirmedMessage(byte _address)
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

        public Byte PumpAndNozzle { get; set; }

        public byte ControlNo
        {
            get
            {
                return 0x30;
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
                data[0] = 0x82;
                data[1] = 0x01;
                data[2] = PumpAndNozzle;
                return data;
            }
        }

        public ushort TransactionId
        {
            get
            {
                return 0;
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