namespace PumpService.Services.Channel.Pumps.Messages.DCR
{
    public class AuthorizeMessage : IMessage
    {
        public AuthorizeMessage(byte _address, byte[] _plate, byte[] _customer)
        {
            SlaveAddress = _address;
            Plate = _plate;
            Customer = _customer;
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
        public Byte PresetType { get; set; }

        public Byte PresetValueFirstByte { get; set; }
        public Byte PresetValueSecondByte { get; set; }
        public Byte PresetValueThirdByte { get; set; }
        public Byte PresetValueFourthByte { get; set; }

        public Byte AuthorizationType { get; set; }
        public Byte[] Plate { get; set; }
        public Byte[] Customer { get; set; }

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
                Byte[] data = new Byte[50];
                data[0] = 0x88;//transaction id
                data[1] = AuthorizationType == 0x01 ? (Byte)0x30 : (Byte)0x07;//length
                data[2] = PumpAndNozzle;
                data[3] = PresetType;//preset type 0:unlimited 1:Money 2:Liter ; was 0x00;
                data[4] = PresetValueFirstByte;//next 4 byte will show money or liter preset values if available
                data[5] = PresetValueSecondByte;
                data[6] = PresetValueThirdByte;
                data[7] = PresetValueFourthByte;//was 0x50 before
                data[8] = AuthorizationType;//0x00;//authorization type : 0:cash

                for (int i = 0; i < Plate.Length; i++)
                {
                    data[9 + i] = Plate[i];
                }

                for (int i = 0; i < Customer.Length; i++)
                {
                    data[17 + i] = Customer[i];
                }

                data[49] = 0x00;

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