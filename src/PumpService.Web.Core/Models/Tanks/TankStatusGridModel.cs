using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Tanks
{
    public partial class TankStatusGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusGridModel_FuelLevelVolume_DisplayName)]
        public decimal FuelLevelVolume { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusGridModel_FuelLevelLength_DisplayName)]
        public decimal FuelLevelLength { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusGridModel_WaterLevelVolume_DisplayName)]
        public decimal WaterLevelVolume { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusGridModel_WaterLevelLength_DisplayName)]
        public decimal WaterLevelLength { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusGridModel_StatusInfoDate_DisplayName)]
        public DateTime StatusInfoDate { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusGridModel_Tank_Code_DisplayName)]
        public string Tank_Code { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusGridModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}