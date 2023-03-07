using PumpService.Services.Channel.Pumps.Messages.Mepsan;
using PumpService.Services.Channel.Streams;
using PumpService.Services.Channel.Utility;
using Serilog;

namespace PumpService.Services.Channel.Pumps.Transport
{
    public class MepsanTransport : SerialTransport
    {
        //private Mepsan device;

        public MepsanTransport(IStreamResource streamResource)
            : base(streamResource)
        {
            CheckFrame = true;  // Perform CRC Check for this  Transport
        }

        public void setDevice()//(Mepsan device)
        {
            //this.device = device;
        }

        public override bool ChecksumsMatch(IMessage message, byte[] messageFrame)
        {
            String pollData = CrcCalc.ByteToHexStr(messageFrame);
            string pData = "";
            string pCRC = "";
            CrcCalc.CRCfilter(pollData, ref pCRC, ref pData);
            string crcData = CrcCalc.Crc16calc(pData.Trim());
            if (pCRC.Trim() != crcData)
                return false;
            else
                return true;
        }

        protected override bool NeedResponse(IMessage message)
        {
            if (message is ACKMessage) return false;
            return true;
        }

        protected override IMessage DummyResponse()
        {
            return new Messages.DCR.DCRResponseMessage();
        }

        public override void Write(IMessage message)
        {
            DiscardInBuffer();

            byte[] frame = BuildMessageFrame(message);

            StreamResource.Write(frame, 0, frame.Length);

            //log.Warn("Mepsan-WrittenTX:" + frame.Join(","));
            Log.Logger.Warning("Mepsan-WrittenTX:" + ByteArrayToString(frame));
        }

        public override byte[] ReadRequest()
        {
            byte[] frame = new byte[0];
            byte[] inBuffer = new byte[1];

            //lock (this)
            // {
            do
            {
                int bytesRead = StreamResource.Read(inBuffer, 0, 1);

                if (bytesRead > 0)
                {
                    frame = frame.Concat(inBuffer).ToArray();
                }
                else
                {
                    //       log.Warn("DCRTransport ReadRequest Başarısız Oldu");
                    break;
                }
            } while (true);
            //}

            if (frame.Length > 0)
                //log.Warn("Mepsan-Read-RX:" + frame.Join(","));
                Log.Logger.Warning("Mepsan-Read-RX:" + ByteArrayToString(frame));
            else
                Log.Logger.Warning("Mepsan-Read-XX:" + "<NONE>");

            return frame;
        }

        public override IMessage ReadResponse<T>()
        {
            byte[] frame = ReadRequest();
            if (frame != null && frame.Length > 0)
            {
                StateEngine.SlaveTxNo = (byte)(frame[1] & 0x0F); // get TX No in control Byte
            }
            IMessage response = CreateResponse<T>(frame);

            return response;
        }

        //gönderilecek olan mesaj oluşturulur #sümer#
        public override byte[] BuildMessageFrame(IMessage message)
        {
            byte[] data = message.ProtocolDataUnit;
            int dataLen;
            if (message.ProtocolDataUnit != null)
                dataLen = message.ProtocolDataUnit.Length + 4;
            else
                dataLen = 1;
            int msgLen = 2 + dataLen;
            byte[] frame = new byte[msgLen];

            byte ctrlNo = (byte)(message.ControlNo | 0x0); // message.TransactionId);
            frame[0] = message.SlaveAddress;
            frame[1] = ctrlNo;
            if (message.ProtocolDataUnit != null)
            {
                message.ProtocolDataUnit.CopyTo(frame, 2);
                byte[] rawData = new Byte[data.Length + 2];
                rawData[0] = frame[0];
                rawData[1] = frame[1];
                data.CopyTo(rawData, 2);
                // frame.Concat(message.ProtocolDataUnit);
                Byte[] crc = CrcCalc.CalcCRC(CrcCalc.ByteToHexStr(rawData)); //).Substring(0,(msgLen-2)*2));
                                                                             //if ((crc[0] == 0xFA) || (crc[1] == 0xFA))
                                                                             //{
                                                                             //    byte[] frame1 = new byte[frame.Length + 1];
                                                                             //    Array.Copy(frame, 0, frame1, 0, frame.Length);
                                                                             //    frame1[frame1.Length - 5] = 0x10;
                                                                             //    crc.CopyTo(frame1, frame1.Length - 4);
                                                                             //    frame1[frame1.Length - 2] = 0x03;
                                                                             //    frame1[frame1.Length - 1] = 0xFA;
                                                                             //    return frame1;
                                                                             //}
                                                                             //else
                                                                             //{
                crc.CopyTo(frame, dataLen - 2);
                frame[msgLen - 2] = 0x03;
                //}
            }
            //frame[msgLen - 1] = 0xFA;

            return CheckDLEIfNeeded(frame);
        }

        private byte[] CheckDLEIfNeeded(byte[] pSrcBuffer)
        {
            List<byte> tFrame = new List<byte>();

            for (int i = 0; i < pSrcBuffer.Length; i++)
            {
                if (pSrcBuffer[i] == 0xFA)
                {
                    tFrame.Add(0x10);
                }
                tFrame.Add(pSrcBuffer[i]);
            }
            tFrame[tFrame.Count() - 1] = 0xFA;
            return tFrame.ToArray<byte>();
        }

        public override void OnValidateResponse(IMessage request, IMessage response)
        {
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", ",");
        }
    }
}