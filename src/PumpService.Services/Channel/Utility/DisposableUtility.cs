namespace PumpService.Services.Channel.Utility
{
    public static class DisposableUtility
    {
        #region Methods

        public static void Dispose<T>(ref T item) where T : class, IDisposable
        {
            if (item != null)
            {
                item.Dispose();
                item = null;
            }
        }

        #endregion Methods
    }
}