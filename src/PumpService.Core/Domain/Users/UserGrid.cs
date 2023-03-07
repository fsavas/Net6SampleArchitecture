using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Core.Domain.Users
{
    public partial class UserGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Users_UserGridModel_Username_DisplayName)]
        public string Username { get; set; }

        #endregion Properties
    }
}