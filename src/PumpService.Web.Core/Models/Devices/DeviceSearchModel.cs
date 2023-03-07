using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Devices
{
    public partial class DeviceSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Devices_DeviceSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}