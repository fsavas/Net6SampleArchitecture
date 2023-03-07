using PumpService.Core.Domain.Devices;
using PumpService.Core.Domain.Pumps;
using PumpService.Core.Domain.Stations;
using PumpService.Core.Domain.Tanks;
using PumpService.Core.Results;
using PumpService.Services.Channel.Pumps;
using PumpService.Services.Channel.Streams;
using System.IO.Ports;

namespace PumpService.Services.Channel
{
    public class ChannelData
    {
        #region Fields

        private List<Tank> _tanks;
        private List<SerialPortAdapter> _serialPortAdapters;
        private List<PumpSerialDevice> _pumpSerialDevices;
        private List<Device> _devices;
        private List<DeviceParameter> _deviceParameters;
        private List<DeviceType> _deviceTypes;
        private Dictionary<string, PumpStatusResult> _pumpStatuses;
        private Dictionary<string, FillingInformationResult> _fillingInformations;
        private Dictionary<string, bool> _isAuthorize;
        private Dictionary<string, bool> _isConfirmPay;
        private Dictionary<string, bool> _isUpdateUnitPrice;
        private List<PumpSales> _continuingPumpSales { get; set; }

        #endregion Fields

        #region Constructor

        public ChannelData()
        {
            _tanks = new List<Tank>();
            _serialPortAdapters = new List<SerialPortAdapter>();
            _pumpSerialDevices = new List<PumpSerialDevice>();
            _continuingPumpSales = new List<PumpSales>();
            _devices = new List<Device>();
            _deviceParameters = new List<DeviceParameter>();
            _deviceTypes = new List<DeviceType>();
            _pumpStatuses = new Dictionary<string, PumpStatusResult>();
            _fillingInformations = new Dictionary<string, FillingInformationResult>();
            _isAuthorize = new Dictionary<string, bool>();
            _isUpdateUnitPrice = new Dictionary<string, bool>();
            _isConfirmPay = new Dictionary<string, bool>();
        }

        #endregion Constructor

        #region Methods

        public void AddTank(Tank tank)
        {
            _tanks.Add(tank);
        }

        public void AddTanks(List<Tank> tanks)
        {
            _tanks.AddRange(tanks);
        }

        public void AddDevice(Device device)
        {
            _devices.Add(device);
        }

        public void AddDevices(List<Device> devices)
        {
            _devices.AddRange(devices);
        }

        public void AddDeviceParameter(DeviceParameter deviceParameter)
        {
            _deviceParameters.Add(deviceParameter);
        }

        public void AddDeviceParameters(List<DeviceParameter> deviceParameters)
        {
            _deviceParameters.AddRange(deviceParameters);
        }

        public void AddDeviceType(DeviceType deviceType)
        {
            _deviceTypes.Add(deviceType);
        }

        public void AddDeviceTypes(List<DeviceType> deviceTypes)
        {
            _deviceTypes.AddRange(deviceTypes);
        }

        public SerialPortAdapter SerialPortAdapter(string portName)
        {
            return SerialPortAdapters.Where(x => x.PortName == portName).FirstOrDefault();
        }

        public void AddSerialPortDefinition(SerialPortDefinition serialPortDefinition)
        {
            if (serialPortDefinition != null)
            {
                string[] ports = SerialPort.GetPortNames();

                if (ports.Contains(serialPortDefinition.PortName))
                {
                    var serialPortAdapter = new SerialPortAdapter(serialPortDefinition);
                    serialPortAdapter.OpenPort();
                    SerialPortAdapters.Add(serialPortAdapter);
                }
            }
        }

        public void AddPumpSerialDevices(List<PumpSerialDevice> pumpSerialDevices)
        {
            if (pumpSerialDevices != null)
            {
                PumpSerialDevices.AddRange(pumpSerialDevices);
            }
        }

        //public void InitializeSerialPortAdapters(List<SerialPortDefinition> serialPortDefinitions)
        //{
        //    if(serialPortDefinitions != null && serialPortDefinitions.Count > 0)
        //    {
        //        string[] ports = SerialPort.GetPortNames();

        //        foreach (var serialPortDefinition in serialPortDefinitions)
        //        {
        //            if (ports.Contains(serialPortDefinition.PortName))
        //            {
        //                var serialPortAdapter = new SerialPortAdapter(serialPortDefinition);
        //                serialPortAdapter.OpenPort();
        //                SerialPortAdapters.Add(serialPortAdapter);
        //            }
        //        }
        //    }
        //}

        public void PumpSalesStarted(PumpSales satis)
        {
            bool satisVar = false;

            foreach (var acikSatis in _continuingPumpSales)
            {
                if (acikSatis.TransactionStartTime == satis.TransactionStartTime &&
                    acikSatis.FillingPoint != null && satis.FillingPoint != null && acikSatis.FillingPoint.Code == satis.FillingPoint.Code)
                    //acikSatis.Pump != null && satis.Pump != null && acikSatis.Pump.PumpId == satis.Pump.PumpId)
                    satisVar = true;
            }

            if (!satisVar)
                _continuingPumpSales.Add(satis);
        }

        public void PumpSalesEnded(PumpSales satis)
        {
            int indis = -1;

            for (int i = 0; i < _continuingPumpSales.Count; i++)
            {
                //if (_continuingPumpSales[i].Pump != null && satis.Pump != null && _continuingPumpSales[i].Pump.PumpId == satis.Pump.PumpId)
                if (_continuingPumpSales[i].FillingPoint != null && satis.FillingPoint != null && _continuingPumpSales[i].FillingPoint.Code == satis.FillingPoint.Code)
                    indis = i;
            }

            if (indis != -1)
            {
                _continuingPumpSales.RemoveAt(indis);
                PumpSalesEnded(satis);//tamamlanan satışın ait olduğu pompa ile ilgili tüm satışlar remove edilsin diye recursive çağrılır
            }
        }

        public bool IsContinuingPumpSales()
        {
            if (_continuingPumpSales.Count > 0)
                return true;
            else
                return false;
        }

        #endregion Methods

        #region Properties

        public List<Tank> Tanks
        {
            get { return _tanks.OrderBy(x => x.Code).ToList(); }
            private set { _tanks = value; }
        }

        public List<SerialPortAdapter> SerialPortAdapters
        {
            get { return _serialPortAdapters; }
            private set { _serialPortAdapters = value; }
        }

        public List<PumpSerialDevice> PumpSerialDevices
        {
            get { return _pumpSerialDevices; }
            private set { _pumpSerialDevices = value; }
        }

        public List<Device> Devices
        {
            get { return _devices; }
            private set { _devices = value; }
        }

        public List<DeviceParameter> DeviceParameters
        {
            get { return _deviceParameters; }
            private set { _deviceParameters = value; }
        }

        public List<DeviceType> DeviceTypes
        {
            get { return _deviceTypes; }
            private set { _deviceTypes = value; }
        }

        public Dictionary<string, PumpStatusResult> PumpStatuses
        {
            get { return _pumpStatuses; }
            set { _pumpStatuses = value; }
        }

        public Dictionary<string, FillingInformationResult> FillingInformations
        {
            get { return _fillingInformations; }
            set { _fillingInformations = value; }
        }

        public Dictionary<string, bool> IsAuthorize
        {
            get { return _isAuthorize; }
            set { _isAuthorize = value; }
        }

        public Dictionary<string, bool> IsConfirmPay
        {
            get { return _isConfirmPay; }
            set { _isConfirmPay = value; }
        }

        public Dictionary<string, bool> IsUpdateUnitPrice
        {
            get { return _isUpdateUnitPrice; }
            set { _isUpdateUnitPrice = value; }
        }

        #endregion Properties
    }

    //todo adını değiştir ayrı dosyaya taşı
    public class OlcumCounter
    {
        public int olcumIntegerMm { get; set; }
        public int counter { get; set; }
    }
}