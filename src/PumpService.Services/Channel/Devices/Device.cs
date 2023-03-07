using PumpService.Services.Channel.Streams;
using PumpService.Services.Channel.Utility;

namespace PumpService.Services.Channel.Devices
{
    public abstract class Device : IDisposable
    {
        #region Fields

        private SerialTransport _transport;

        #endregion Fields

        #region Constructor

        internal Device(SerialTransport transport)
        {
            _transport = transport;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Gets the Modbus Transport.
        /// </summary>
        /// <value>The transport.</value>
        public SerialTransport Transport
        {
            get
            {
                return _transport;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DisposableUtility.Dispose(ref _transport);
        }

        #endregion Methods
    }
}