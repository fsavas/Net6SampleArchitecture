using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Stations
{
    public partial class SerialPortDefinitionModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortName_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortName_DisplayName)]
        public string PortName { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_BaudRate_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_BaudRate_DisplayName)]
        public int BaudRate { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_DataBits_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_DataBits_DisplayName)]
        public int DataBits { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_Parity_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_Parity_DisplayName)]
        public int Parity { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_StopBits_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_StopBits_DisplayName)]
        public int StopBits { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_ReadTimeout_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_ReadTimeout_DisplayName)]
        public int ReadTimeout { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_WriteTimeout_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_WriteTimeout_DisplayName)]
        public int WriteTimeout { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortTypeId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortTypeId_DisplayName)]
        public long? PortTypeId { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_IsActive_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_Parity_DisplayName)]
        public List<SelectListItemModel> Parities { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_StopBits_DisplayName)]
        public List<SelectListItemModel> StopBitses { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortTypes_DisplayName)]
        public List<SelectListItemModel> PortTypes { get; set; }

        #endregion Properties
    }
}