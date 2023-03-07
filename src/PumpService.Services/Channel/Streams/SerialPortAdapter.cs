using PumpService.Core.Defaults;
using PumpService.Core.Domain.Stations;
using PumpService.Services.Channel.Utility;
using Serilog;
using System.IO.Ports;

namespace PumpService.Services.Channel.Streams
{
    public class SerialPortAdapter : IStreamResource
    {
        #region Fields

        public SerialPort _serialPort;

        #endregion Fields

        #region Constructor

        public SerialPortAdapter(SerialPortDefinition serialPortDefinition)
        {
            _serialPort = new SerialPort(serialPortDefinition.PortName);

            this.BaudRate = serialPortDefinition.BaudRate;
            this.DataBits = serialPortDefinition.DataBits;
            this.StopBits = serialPortDefinition.StopBits;
            this.Parity = serialPortDefinition.Parity;
            this.ReadTimeout = serialPortDefinition.ReadTimeout;
            this.WriteTimeout = serialPortDefinition.WriteTimeout;

            this.NewLine = ChannelKeys.NewLine;
        }

        #endregion Constructor

        #region Methods

        public int OpenPort()
        {
            if (_serialPort == null)
            {
                Log.Logger.ForContext("LogKey", LogKeys.ComPortEmpty).Warning("PortName=" + PortName + " Message=" + LogKeys.ComPortEmpty);

                return -1;
            }

            int portCounter = 0;

            Log.Logger.ForContext("LogKey", LogKeys.ComPortClosing).Information("PortName=" + PortName + " Message=" + LogKeys.ComPortClosing);

            if (_serialPort.IsOpen)
            {
                while (portCounter < 20)
                {
                    try
                    {
                        _serialPort.Close();
                        Log.Logger.ForContext("LogKey", LogKeys.ComPortClosed).Information("PortName=" + PortName + " Message=" + LogKeys.ComPortClosed);
                        break;
                    }
                    catch (Exception e)
                    {
                        portCounter++;
                        Log.Logger.ForContext("LogKey", LogKeys.ComPortCloseException).Error("PortName=" + PortName + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
                        Thread.Sleep(1000);
                    }
                }
            }

            portCounter = 0;

            if (!_serialPort.IsOpen)
            {
                Log.Logger.ForContext("LogKey", LogKeys.ComPortOpening).Information("PortName=" + PortName + " Message=" + LogKeys.ComPortOpening);

                while (portCounter < 20)
                {
                    try
                    {
                        _serialPort.Open();
                        Log.Logger.ForContext("LogKey", LogKeys.ComPortOpened).Information("PortName=" + PortName + " Message=" + LogKeys.ComPortOpened);

                        return 0;
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            
                            _serialPort.Close();
                            Log.Logger.ForContext("LogKey", LogKeys.ComPortClosed).Information("PortName=" + PortName + " Message=" + LogKeys.ComPortClosed);
                        }
                        catch (Exception ex)
                        {
                            Log.Logger.ForContext("LogKey", LogKeys.ComPortCloseException).Error("PortName=" + PortName + " Message=" + ex.Message + " StackTrace=" + ex.StackTrace);
                        }

                        portCounter++;

                        Log.Logger.ForContext("LogKey", LogKeys.ComPortOpenException).Error("PortName=" + PortName + " Message=" + e.Message + " StackTrace=" + e.StackTrace);

                        Thread.Sleep(1000);
                    }
                }
            }

            return -1;
        }

        public void ClosePort()
        {
            if (_serialPort == null)
            {
                Log.Logger.ForContext("LogKey", LogKeys.ComPortEmpty).Warning("PortName=" + PortName + " Message=" + LogKeys.ComPortEmpty);

                return;
            }

            int portCounter = 0;

            Log.Logger.ForContext("LogKey", LogKeys.ComPortClosing).Information("PortName=" + PortName + " Message=" + LogKeys.ComPortClosing);

            while (portCounter < 20)
            {
                try
                {
                    _serialPort.Close();
                    Log.Logger.ForContext("LogKey", LogKeys.ComPortClosed).Information("PortName=" + PortName + " Message=" + LogKeys.ComPortClosed);

                    break;
                }
                catch (Exception e)
                {
                    portCounter++;
                    Log.Logger.ForContext("LogKey", LogKeys.ComPortCloseException).Error("PortName=" + PortName + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
                    Thread.Sleep(1000);
                }
            }
        }

        public bool IsOpen()
        {
            if (_serialPort != null)
                return _serialPort.IsOpen;
            else
                return false;
        }

        public void DiscardInBuffer()
        {
            try
            {
                _serialPort.DiscardInBuffer();
            }
            catch (Exception e)
            {
                Log.Logger.ForContext("LogKey", LogKeys.DiscardInBufferException).Error("PortName=" + PortName + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
            }
        }

        public void DiscardOutBuffer()
        {
            try
            {
                _serialPort.DiscardOutBuffer();
            }
            catch (Exception e)
            {
                Log.Logger.ForContext("LogKey", LogKeys.DiscardOutBufferException).Error("PortName=" + PortName + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
            }
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            _serialPort.ReadTimeout = ReadTimeout;

            try
            {
                return _serialPort.Read(buffer, offset, count);
            }
            catch (Exception e)
            {
                //Log.Logger.ForContext("LogKey", LogKeys.SerialPortReadBufferException).Error("PortName=" + PortName + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
                return 0;
            }
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            _serialPort.WriteTimeout = WriteTimeout;

            try
            {
                _serialPort.Write(buffer, offset, count);
            }
            catch (TimeoutException e)
            {
                Log.Logger.ForContext("LogKey", LogKeys.SerialPortWriteBufferException).Error("PortName=" + PortName + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DisposableUtility.Dispose(ref _serialPort);
        }

        #endregion Methods

        #region Properties

        public int InfiniteTimeout
        {
            get { return SerialPort.InfiniteTimeout; }
        }

        public string PortName
        {
            get { return _serialPort.PortName; }
            set { _serialPort.PortName = value; }
        }

        public int BaudRate
        {
            get { return _serialPort.BaudRate; }
            set { _serialPort.BaudRate = value; }
        }

        public int DataBits
        {
            get { return _serialPort.DataBits; }
            set { _serialPort.DataBits = value; }
        }

        public StopBits StopBits
        {
            get { return _serialPort.StopBits; }
            set { _serialPort.StopBits = value; }
        }

        public Parity Parity
        {
            get { return _serialPort.Parity; }
            set { _serialPort.Parity = value; }
        }

        public int ReadTimeout
        {
            get { return _serialPort.ReadTimeout; }
            set { _serialPort.ReadTimeout = value; }
        }

        public int WriteTimeout
        {
            get { return _serialPort.WriteTimeout; }
            set { _serialPort.WriteTimeout = value; }
        }

        public string NewLine
        {
            get { return _serialPort.NewLine; }
            set { _serialPort.NewLine = value; }
        }

        #endregion Properties
    }
}