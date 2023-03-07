using PumpService.Core.Defaults;
using PumpService.Services.Channel.Pumps.Messages.Mepsan;
using PumpService.Services.Channel.Tanks.Messages;
using PumpService.Services.Channel.Utility;
using System.Diagnostics;
using System.Globalization;

namespace PumpService.Services.Channel.Streams
{
    public abstract class SerialTransport : IDisposable
    {
        #region Fields

        private bool _checkFrame = true;
        private int _retries = ChannelKeys.DefaultRetries;
        private int MepsanWait = ChannelKeys.MepsanWait;
        private int MepsanProbeWait = ChannelKeys.MepsanProbeWait;
        private int AsisProbeWait = ChannelKeys.AsisProbeWait;
        private int TeosisWait = ChannelKeys.TeosisWait;
        private IStreamResource _streamResource;

        #endregion Fields

        #region Constructor

        public SerialTransport(IStreamResource streamResource)

        {
            Debug.Assert(streamResource != null, "Argument streamResource cannot be null.");

            _streamResource = streamResource;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Number of times to retry sending message after encountering a failure such as an IOException,
        /// TimeoutException, or a corrupt message.
        /// </summary>
        public int Retries
        {
            get { return _retries; }
            set { _retries = value; }
        }

        /// <summary>
        /// Gets the stream resource.
        /// </summary>
        public IStreamResource StreamResource
        {
            get
            {
                return _streamResource;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DisposableUtility.Dispose(ref _streamResource);
        }

        protected virtual bool NeedResponse(IMessage message)
        {
            return true;
        }

        protected virtual IMessage DummyResponse()
        {
            return null;
        }

        public virtual T UnicastMessage<T>(IMessage message) where T : IMessage, new()
        {
            IMessage response = null;
            int attempt = 1;

            bool success = false;

            do
            {
                try
                {
                    lock (_streamResource)
                    {
                        Write(message);

                        //Asis Probe için bekleme süresi (Timeouts)
                        if (message.GetType().Equals(typeof(AsisRequestTankStatusMessage)) || message.GetType().Equals(typeof(AsisSetAddressMessage)) || message.GetType().Equals(typeof(AsisRequestVersionMessage)))
                            Thread.Sleep(AsisProbeWait);
                        else if (message.GetType().Equals(typeof(ACKMessage))
                                  || message.GetType().Equals(typeof(AllowedNozzleNumberMessage))
                                  || message.GetType().Equals(typeof(AuthorizeNozzleMessage))
                                  || message.GetType().Equals(typeof(Pumps.Messages.Mepsan.MepsanResponseMessage))
                                  || message.GetType().Equals(typeof(MepsanStatusMessage))
                                  || message.GetType().Equals(typeof(MepsanStopMessage))
                                  || message.GetType().Equals(typeof(PollMessage))
                                  || message.GetType().Equals(typeof(RequestEndOfFillingMessage))
                                  || message.GetType().Equals(typeof(RequestNozzleTotalizerMessage))
                                  || message.GetType().Equals(typeof(ResetMessage))
                                  || message.GetType().Equals(typeof(UpdatePriceMessage)))
                            Thread.Sleep(MepsanWait);
                        else if (message.GetType().Equals(typeof(TeosisMessage)) || message.GetType().Equals(typeof(TeosisResponseMessage)))
                            Thread.Sleep(TeosisWait);
                        else if (message.GetType().Equals(typeof(MepsanMessage)) || message.GetType().Equals(typeof(Tanks.Messages.MepsanResponseMessage)))
                            Thread.Sleep(MepsanProbeWait);

                        if (NeedResponse(message))
                            response = ReadResponse<T>();
                        else
                            response = (T)DummyResponse();

                        ValidateResponse(message, response);
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    if (e is FormatException ||
                        e is NotImplementedException ||
                        e is TimeoutException ||
                        e is IOException)
                    {
                        //log.Warn("UniCastMessage Exception1" + "StackTrace=" + e.StackTrace + "Message=" + e.Message);

                        if (attempt++ > _retries)
                            throw;
                    }
                    else
                    {
                        //log.Warn("UniCastMessage Exception2" + "StackTrace=" + e.StackTrace + "Message=" + e.Message);
                        throw;
                    }
                }
            } while (!success);

            return (T)response;
        }

        public IMessage CreateResponse<T>(byte[] frame) where T : IMessage, new()
        {
            if (frame == null)
                return null;

            IMessage response;
            response = MessageFactory.CreateMessage<T>(frame);

            if (CheckFrame && frame.Length > 3) // 3 ten büyükse CRC check Yapılacak
            {
                if (!ChecksumsMatch(response, frame))
                {
                    string errorMessage = String.Format(CultureInfo.InvariantCulture, "Checksums failed to match {0} != {1}", response.MessageFrame.Join(", "), frame.Join(", "));

                    throw new IOException(errorMessage);
                }
            }
            return response;
        }

        public void ValidateResponse(IMessage request, IMessage response)
        {
            OnValidateResponse(request, response);
        }

        public virtual void OnValidateResponse(IMessage request, IMessage response)
        {
            // no-op
        }

        public abstract byte[] ReadRequest();

        public abstract IMessage ReadResponse<T>() where T : IMessage, new();

        public abstract byte[] BuildMessageFrame(IMessage message);

        /// <summary>
        /// Gets or sets a value indicating whether LRC/CRC frame checking is performed on messages.
        /// </summary>
        public bool CheckFrame
        {
            get { return _checkFrame; }
            set { _checkFrame = value; }
        }

        public void DiscardInBuffer()
        {
            StreamResource.DiscardInBuffer();
        }

        public void DiscardOutBuffer()
        {
            StreamResource.DiscardOutBuffer();
        }

        public virtual void Write(IMessage message)
        {
        }

        public abstract bool ChecksumsMatch(IMessage message, byte[] messageFrame);

        #endregion Methods
    }
}