using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Security
{
    public partial class PermissionSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Security_PermissionSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Security_PermissionSearchModel_RoleId_DisplayName)]
        public long RoleId { get; set; }

        #endregion Properties
    }
}