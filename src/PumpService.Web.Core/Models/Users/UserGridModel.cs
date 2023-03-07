using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Users
{
    public partial class UserGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserGridModel_Username_DisplayName)]
        public string Username { get; set; }

        #endregion Properties
    }
}