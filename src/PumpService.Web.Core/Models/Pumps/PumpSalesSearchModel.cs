using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Pumps
{
    public partial class PumpSalesSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesSearchModel_TransactionStartTime_DisplayName)]
        public DateTime? TransactionStartTime { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_PumpSalesSearchModel_TransactionEndTime_DisplayName)]
        public DateTime? TransactionEndTime { get; set; }

        #endregion Properties
    }
}