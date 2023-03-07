namespace PumpService.Services.Channel
{
    public enum PumpState
    {
        Idle,
        Reset,
        AuthorizeNozzle,
        Price,
        SetNozzleNumber
    }

    #region Methods

    public static class StateEngine
    {
        public static byte _txId = 0;

        public static byte SlaveTxNo
        {
            get
            {
                if (_txId == 0xF) _txId = 0;
                return _txId;
            }
            set { _txId = value; }
        }

        public static byte MasterTxNo = 0;
        public static PumpState State;
    }

    #endregion Methods
}