using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Core.Domain.Security
{
    public partial class PermissionGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Security_PermissionGridModel_Code_DisplayName)]
        public string Code { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Security_PermissionGridModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}