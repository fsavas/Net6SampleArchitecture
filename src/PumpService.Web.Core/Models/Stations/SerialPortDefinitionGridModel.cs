using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Stations
{
    public partial class SerialPortDefinitionGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_PortName_DisplayName)]
        public string PortName { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_BaudRate_DisplayName)]
        public int BaudRate { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_DataBits_DisplayName)]
        public int DataBits { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_Parity_DisplayName)]
        public string Parity { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_StopBits_DisplayName)]
        public string StopBits { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_ReadTimeout_DisplayName)]
        public int ReadTimeout { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_WriteTimeout_DisplayName)]
        public int WriteTimeout { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_PortType_Description_DisplayName)]
        public string PortType_Description { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}