using System.Collections;

namespace PumpService.Services.Channel.Pumps.Messages.DCR
{
    public class SuspendMessage : IMessage
    {
        public SuspendMessage(byte _address, Byte pumpId, Byte nozzleId)
        {
            SlaveAddress = _address;

            #region Construct PumpAndNozzle from pumpId and nozzleId

            var pumpBits = new BitArray(new byte[] { pumpId });
            var nozzleBits = new BitArray(new byte[] { nozzleId });
            var combinedBits = new BitArray(8);

            int index = 0;
            //nozzleId bilgisinin ilk dört biti LSD(sağdaki 4 bit) olarak atanır
            for (int i = 0; i < 4; i++)
            {
                combinedBits[index++] = nozzleBits[i];
            }
            //pumpId bilgisinin ilk dört biti MSD(soldaki 4 bit) olarak atanır
            for (int i = 0; i < 4; i++)
            {
                combinedBits[index++] = pumpBits[i];
            }

            byte[] new_bytes = new byte[1];
            combinedBits.CopyTo(new_bytes, 0);
            PumpAndNozzle = new_bytes[0];

            #endregion Construct PumpAndNozzle from pumpId and nozzleId
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
                Byte[] data = new Byte[4];
                data[0] = 0x84;//transaction code
                data[1] = 0x02;//length
                data[2] = PumpAndNozzle;
                data[3] = 0x0;//suspend
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