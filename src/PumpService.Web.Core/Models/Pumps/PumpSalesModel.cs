using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Pumps
{
    public partial class PumpSalesModel : BaseEntityModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesModel_Amount_DisplayName)]
        public virtual decimal Amount { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesModel_PumpQuantity_DisplayName)]
        public virtual decimal PumpQuantity { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesModel_NetQuantity_DisplayName)]
        public virtual decimal NetQuantity { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesModel_UnitPrice_DisplayName)]
        public virtual decimal UnitPrice { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesModel_TransactionStartTime_DisplayName)]
        public virtual DateTime? TransactionStartTime { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesModel_TransactionEndTime_DisplayName)]
        public virtual DateTime? TransactionEndTime { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesModel_FillingPointId_DisplayName)]
        public long FillingPointId { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesModel_NozzleId_DisplayName)]
        public long NozzleId { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesModel_ProductId_DisplayName)]
        public long ProductId { get; set; }

        #endregion Properties
    }
}