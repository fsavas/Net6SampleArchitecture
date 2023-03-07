using PumpService.Services.Channel.Utility;

namespace PumpService.Services.Channel.Pumps.Messages.DCR
{
    public class DCRResponseMessage : IMessage
    {
        public byte SlaveAddress
        {
            get;
            set;
        }

        public byte[] MessageFrame
        {
            get; set;
        }

        public byte[] ProtocolDataUnit
        {
            get { throw new NotImplementedException(); }
        }

        public ushort TransactionId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsIdleMessage()
        {
            if (MessageFrame.Length == 0) return false;
            String st = CrcCalc.ByteToHexStr(MessageFrame);
            if ((st[2] == '2') || (st[2] == '3'))
                return false;
            else
                return true;
        }

        internal bool IsEOTMessage()
        {
            if (MessageFrame.Length == 0) return false;
            String st = CrcCalc.ByteToHexStr(MessageFrame);
            if ((st[2] == '7'))
                return true;
            else
                return false;
        }

        internal bool IsAcknowledgeMessage()
        {
            if (MessageFrame.Length == 0) return false;
            String st = CrcCalc.ByteToHexStr(MessageFrame);
            if ((st[2] == 'C'))
                return true;
            else
                return false;
        }

        public bool IsPumpStatusMessage()
        {
            if (MessageFrame.Length == 0) return false;
            String st = CrcCalc.ByteToHexStr(MessageFrame);

            if ((st[4] == '8') && (st[5] == '0'))
                return true;
            else
                return false;
        }

        public bool IsFillingMessage()
        {
            if (MessageFrame.Length == 0) return false;
            String st = CrcCalc.ByteToHexStr(MessageFrame);

            if ((st[4] == '8') && (st[5] == '3'))
                return true;
            else
                return false;
        }

        public void Initialize(byte[] frame)
        {
            MessageFrame = new Byte[frame.Length];
            frame.CopyTo(MessageFrame, 0);
            if (frame.Length > 6)
            {
                frame = RemoveCodeTransparency(frame);
                BuildTransactions(frame);
            }
        }

        private Byte[] RemoveCodeTransparency(Byte[] buf)
        {
            int i = 0, j = 0;
            bool dleFound = false;
            byte[] data = new byte[buf.Length - 1];
            // Check if there is DLE 0x10 in the data before Any  0xFA
            while (i < buf.Length - 1)
            {
                if (buf[i] == 0x10 && buf[i + 1] == 0xFA)
                {
                    i++;
                    dleFound = true;
                }
                else
                    data[j++] = buf[i++];
            }
            if (!dleFound)
            {
                Array.Resize(ref data, buf.Length);
            }
            data[j] = buf[i];
            return data;
        }

        // Code Transparancey Handle Edelim.
        private void BuildTransactions(byte[] frame)
        {
            byte[] bufData = new byte[frame.Length - 6];
            Array.Copy(frame, 2, bufData, 0, frame.Length - 6);
            _transactions = new List<TransactionData>();
            while (true)
            {
                TransactionData trx = new TransactionData(bufData);
                Transactions.Add(trx);
                bufData = bufData.Skip(2 + trx.Length).ToArray();
                if (bufData.Length == 0) break;
            }
        }

        private List<TransactionData> _transactions;

        public List<TransactionData> Transactions
        {
            get { return _transactions; }
            set { _transactions = value; }
        }

        public byte ControlNo
        {
            get
            {
                byte ctrlByte = MessageFrame[1];
                return (byte)(ctrlByte & 0xF0);
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}