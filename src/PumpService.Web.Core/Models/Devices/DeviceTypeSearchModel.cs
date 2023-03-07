using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Devices
{
    public partial class DeviceTypeSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_DeviceTypes_DeviceTypeSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}