using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Pumps
{
    public partial class NozzleSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleSearchModel_ProductId_DisplayName)]
        public long ProductId { get; set; }

        #endregion Properties
    }
}