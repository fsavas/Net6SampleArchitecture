using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Users
{
    public partial class UserModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserModel_Username_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserModel_Username_DisplayName)]
        public string Username { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserModel_Password_Required)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserModel_Password_RegularExpression)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserModel_Password_DisplayName)]
        public string Password { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserModel_AvailableRoles_DisplayName)]
        public IList<SelectListItemModel>? AvailableRoles { get; set; }

        //[Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserModel_SelectedRoles_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserModel_SelectedRoles_DisplayName)]
        public IList<SelectListItemModel>? SelectedRoles { get; set; }

        #endregion Properties
    }
}