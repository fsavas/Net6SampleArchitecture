using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Security
{
    public partial class RoleSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Security_RoleSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}