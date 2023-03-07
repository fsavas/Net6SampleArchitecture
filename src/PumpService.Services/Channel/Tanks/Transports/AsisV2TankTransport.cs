using PumpService.Services.Channel.Streams;
using System.Diagnostics;

namespace PumpService.Services.Channel.Tanks.Probes.Transport
{
    public class AsisV2TankTransport : SerialTransport
    {
        #region Constructor

        public AsisV2TankTransport(IStreamResource streamResource)
            : base(streamResource)
        {
            Debug.Assert(streamResource != null, "Argument streamResource cannot be null.");
        }

        #endregion Constructor

        #region Methods

        private static byte CalcCRC(byte[] data)
        {
            byte num = 0x11;
            byte num2 = 0;
            for (int i = 0; i < num; i++)
            {
                num2 = (byte)(num2 ^ data[i]);//exclusive or
            }
            return Convert.ToByte((int)(0xff - num2));
        }

        public override bool ChecksumsMatch(IMessage message, byte[] messageFrame)
        {
            return true; //  throw new NotImplementedException();
        }

        public override void OnValidateResponse(IMessage request, IMessage response)
        {
        }

        public virtual byte[] Read(int count)
        {
            byte[] frameBytes = new byte[count];
            int numBytesRead = 0;

            while (numBytesRead != count)
                numBytesRead += StreamResource.Read(frameBytes, numBytesRead, count - numBytesRead);

            return frameBytes;
        }

        public override byte[] ReadRequest()
        {
            return ReadRequestBlock();
        }

        private byte[] ReadRequestBlock()
        {
            byte[] frame = new byte[0];
            byte[] inBuffer = new byte[1];
            bool messageRead = false;

            // _logger.Info("Start Reading");

            do
            {
                int bytesRead = StreamResource.Read(inBuffer, 0, 1);
                //      _logger.DebugFormat("Bytes Read:{0}", bytesRead);
                if (bytesRead > 0)
                {
                    frame = frame.Concat(inBuffer).ToArray();
                    // _logger.DebugFormat("FRAME:{0}", frame.Join(", "));
                    if (frame.Length == 18)
                        messageRead = true;
                }
                else
                    break;
            } while (!messageRead);

            if (messageRead)
                return frame;
            else
                return null;
        }

        public override IMessage ReadResponse<T>()
        {
            return CreateResponse<T>(ReadRequestBlock());
        }

        public override byte[] BuildMessageFrame(IMessage message)
        {
            // AsisRequestTankStatusMessage msg = (AsisRequestTankStatusMessage)message;

            byte[] buffer = message.MessageFrame; /* new byte[] {
                        250, 0, 0, 0, 0, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                         0xff, 0xff
                        };*/

            //  buffer[2] = message.SlaveAddress;
            //  buffer[3] = Convert.ToByte(AsisProbeCommandEnums.EndOfComm);
            //  buffer[4] = Convert.ToByte(AsisProbeCommandEnums.Nominal);
            byte crc = CalcCRC(buffer);
            buffer[0x11] = crc;

            //byte[] data = frame;
            //data[0x11] = Utils.CalcCRC(data);
            // msg.Initialize(frame);
            //_logger.InfoFormat("Sent message:{0}", msg.ToString());
            //   _logger.DebugFormat("Send Message:{0}", buffer.Join(","));

            return buffer;
        }

        public override void Write(IMessage message)
        {
            DiscardInBuffer();

            byte[] frame = BuildMessageFrame(message);
            //   _logger.InfoFormat("TX: {0}", frame.Join(", "));
            StreamResource.Write(frame, 0, frame.Length);
        }

        #endregion Methods
    }
}