using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Core.Domain.Pumps
{
    public partial class PumpSalesGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_Amount_DisplayName)]
        public decimal Amount { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_PumpQuantity_DisplayName)]
        public decimal PumpQuantity { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_NetQuantity_DisplayName)]
        public decimal NetQuantity { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_UnitPrice_DisplayName)]
        public decimal UnitPrice { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_TransactionStartTime_DisplayName)]
        public DateTime? TransactionStartTime { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_TransactionEndTime_DisplayName)]
        public DateTime? TransactionEndTime { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_FillingPoint_Code_DisplayName)]
        public string FillingPoint_Code { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_Nozzle_Address_DisplayName)]
        public string Nozzle_Address { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_Product_Name_DisplayName)]
        public string Product_Name { get; set; }

        #endregion Properties
    }
}