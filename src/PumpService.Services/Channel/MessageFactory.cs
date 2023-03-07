namespace PumpService.Services.Channel
{
    public static class MessageFactory
    {
        #region Methods

        public static T CreateMessage<T>(byte[] frame) where T : IMessage, new()
        {
            IMessage message = new T();
            message.Initialize(frame);

            return (T)message;
        }

        #endregion Methods
    }
}