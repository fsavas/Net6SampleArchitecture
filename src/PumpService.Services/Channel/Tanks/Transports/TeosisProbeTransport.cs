using PumpService.Services.Channel.Streams;
using PumpService.Services.Channel.Tanks.Messages;
using System.Diagnostics;

namespace PumpService.Services.Channel.Tanks.Probes.Transport
{
    public class TeosisProbeTransport : SerialTransport
    {
        #region Constructor

        public TeosisProbeTransport(IStreamResource streamResource)
            : base(streamResource)
        {
            Debug.Assert(streamResource != null, "Argument streamResource cannot be null.");
        }

        #endregion Constructor

        #region Methods

        public override bool ChecksumsMatch(IMessage message, byte[] messageFrame)
        {
            TeosisDDAMessage msg = (TeosisDDAMessage)message;
            ushort crc = msg.CalculateCRC(message.ProtocolDataUnit);
            int crcData = msg.GetCrcFromMessage(message.ProtocolDataUnit);

            if (crcData == crc) return true;
            else
                return false;
        }

        public override void OnValidateResponse(IMessage request, IMessage response)
        {
        }

        public virtual byte[] Read(int count)
        {
            byte[] frameBytes = new byte[count];
            int numBytesRead = 0;

            while (numBytesRead != count)
            {
                numBytesRead += StreamResource.Read(frameBytes, numBytesRead, count - numBytesRead);
            }

            return frameBytes;
        }

        public override byte[] ReadRequest()
        {
            return ReadRequestBlock();
        }

        private byte[] ReadRequestBlock()
        {
            bool etxread = false;
            int crccount = 0;

            byte[] frame = new byte[0];
            byte[] inBuffer = new byte[1];
            bool messageRead = false;

            // _logger.Info("Start Reading");

            do
            {
                int bytesRead = StreamResource.Read(inBuffer, 0, 1);
                //    _logger.DebugFormat("Bytes Read:{0}", bytesRead);
                if (bytesRead > 0)
                {
                    frame = frame.Concat(inBuffer).ToArray();
                    //   _logger.DebugFormat("FRAME:{0}", frame.Join(", "));

                    if (inBuffer[0] == 0x03)
                    { // etx read
                        etxread = true;
                        // read 5 more bytes;
                        crccount = 0;
                    }

                    if (etxread) crccount++;
                    if (etxread && crccount > 5)
                        messageRead = true;
                }
                else
                {
                    //log.Warn("TeosisProbeTransport ReadRequestBlock Başarısız Oldu");
                    break;
                }
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
            byte[] data = message.ProtocolDataUnit;
            byte[] buffer = new byte[2];
            data.CopyTo(buffer, 0);

            //  _logger.DebugFormat("Send Message:{0}", buffer.Join(","));

            return buffer;
        }

        public override void Write(IMessage message)
        {
            DiscardInBuffer();

            byte[] frame = BuildMessageFrame(message);
            // _logger.InfoFormat("TX: {0}", frame.Join(", "));
            StreamResource.Write(frame, 0, frame.Length);
        }

        #endregion Methods
    }
}