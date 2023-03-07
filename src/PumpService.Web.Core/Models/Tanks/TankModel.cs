using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Tanks
{
    public partial class TankModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_Code_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_Code_DisplayName)]
        public string Code { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddress_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddress_DisplayName)]
        public int ProbeAddress { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_SerialPortDefinitionId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_SerialPortDefinitionId_DisplayName)]
        public long? SerialPortDefinitionId { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_Capacity_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_Capacity_DisplayName)]
        public decimal Capacity { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_Diameter_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_Diameter_DisplayName)]
        public int Diameter { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProductId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProductId_DisplayName)]
        public long? ProductId { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeTypeId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeTypeId_DisplayName)]
        public long? ProbeTypeId { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeTypes_DisplayName)]
        public List<SelectListItemModel> ProbeTypes { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_MeasurementPeriod_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_MeasurementPeriod_DisplayName)]
        public int? MeasurementPeriod { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeLength_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeLength_DisplayName)]
        public int? ProbeLength { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_TankGroupNo_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_TankGroupNo_DisplayName)]
        public string TankGroupNo { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_GruptakiAktifTank_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_GruptakiAktifTank_DisplayName)]
        public bool GruptakiAktifTank { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_LowFuelAlarm_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_LowFuelAlarm_DisplayName)]
        public decimal? LowFuelAlarm { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_HeightLimitBetweenTwoTankStatus_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_HeightLimitBetweenTwoTankStatus_DisplayName)]
        public decimal? HeightLimitBetweenTwoTankStatus { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_IsDetectAutoFilling_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_IsDetectAutoFilling_DisplayName)]
        public bool IsDetectAutoFilling { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_WaterOffset_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_WaterOffset_DisplayName)]
        public decimal? WaterOffset { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_FuelOffset_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_FuelOffset_DisplayName)]
        public decimal? FuelOffset { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumber_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumber_DisplayName)]
        public string ProbeSerialNumber { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumberApplied_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumberApplied_DisplayName)]
        public bool ProbeSerialNumberApplied { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddressAsis_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddressAsis_DisplayName)]
        public short? ProbeAddressAsis { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_IsActive_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}