using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Tanks
{
    public partial class TankStatusSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankStatusSearchModel_TankId_DisplayName)]
        public long? TankId { get; set; }

        #endregion Properties
    }
}