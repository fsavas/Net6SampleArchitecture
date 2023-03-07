using PumpService.Services.Channel.Utility;

namespace PumpService.Services.Channel
{
    public class TransactionData
    {
        #region Fields

        private byte _transactionId;
        private byte _length;
        private byte[] _data;

        #endregion Fields

        #region Constructor

        //gönderilen byte[] içerisindeki transaction data ilgili bölümlere ayrılır#sümer#
        public TransactionData(byte[] frame)
        {
            _transactionId = frame[0];
            _length = frame[1];
            _data = new byte[_length];
            Array.Copy(frame, 2, _data, 0, _length);
        }

        #endregion Constructor

        #region Methods

        public String DataAsString
        {
            get { return CrcCalc.ByteToHexStr(_data); }
        }

        #endregion Methods

        #region Properties

        public byte TransactionId
        {
            get { return _transactionId; }
            set { _transactionId = value; }
        }

        public byte Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        #endregion Properties
    }
}