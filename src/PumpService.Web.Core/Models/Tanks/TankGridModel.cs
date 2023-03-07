using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Tanks
{
    public partial class TankGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankGridModel_Code_DisplayName)]
        public string Code { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankGridModel_ProbeAddress_DisplayName)]
        public int ProbeAddress { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankGridModel_SerialPortDefinitionPortName_DisplayName)]
        public string SerialPortDefinitionPortName { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankGridModel_Capacity_DisplayName)]
        public decimal Capacity { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankGridModel_Diameter_DisplayName)]
        public int Diameter { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankGridModel_Product_Name_DisplayName)]
        public string Product_Name { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankGridModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}