using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Security
{
    public partial class PermissionModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Security_PermissionModel_Code_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Security_PermissionModel_Code_DisplayName)]
        public string Code { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Security_PermissionModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Security_PermissionModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}