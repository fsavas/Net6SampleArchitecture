namespace PumpService.Core.Defaults
{
    public static class ChannelKeys
    {
        public const string KeySeperator = "-";
        public const string NewLine = "\r\n";
        public const int DefaultRetries = 1;
        public const int MepsanWait = 25;
        public const int AsisProbeWait = 400;
        public const int TeosisWait = 50;
        public const int MepsanProbeWait = 50;

        #region Service

        public const string PumpStatus = "PumpStatus";

        #endregion Service
    }
}