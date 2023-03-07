using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Tanks
{
    public partial class TankStatusModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelVolume_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelVolume_DisplayName)]
        public decimal FuelLevelVolume { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelLength_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelLength_DisplayName)]
        public decimal FuelLevelLength { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelVolume_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelVolume_DisplayName)]
        public decimal WaterLevelVolume { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelLength_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelLength_DisplayName)]
        public decimal WaterLevelLength { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_StatusInfoDate_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_StatusInfoDate_DisplayName)]
        public DateTime StatusInfoDate { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_OlcumSebebiId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_OlcumSebebiId_DisplayName)]
        public long? OlcumSebebiId { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_OlcumSebebis_DisplayName)]
        public List<SelectListItemModel> OlcumSebebis { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_SatisMiktari_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_SatisMiktari_DisplayName)]
        public decimal? SatisMiktari { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LT_Kalibrasyon_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LT_Kalibrasyon_DisplayName)]
        public decimal FuelLevel_LT_Kalibrasyon { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LTNet_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LTNet_DisplayName)]
        public decimal? FuelLevel_LTNet { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_TankId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_TankId_DisplayName)]
        public long? TankId { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_PompaSatisId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_PompaSatisId_DisplayName)]
        public long? PompaSatisId { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_TankDolumId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_TankDolumId_DisplayName)]
        public long? TankDolumId { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_IsActive_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}