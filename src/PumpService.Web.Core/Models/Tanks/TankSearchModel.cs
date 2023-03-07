using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Tanks
{
    public partial class TankSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Tanks_TankSearchModel_Code_DisplayName)]
        public string Code { get; set; }

        #endregion Properties
    }
}