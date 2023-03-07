using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Users
{
    public partial class UserSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserSearchModel_Username_DisplayName)]
        public string Username { get; set; }

        #endregion Properties
    }
}