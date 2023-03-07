using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Devices
{
    public partial class DeviceParameterSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_DeviceParameters_DeviceParameterSearchModel_Value_DisplayName)]
        public string Value { get; set; }

        #endregion Properties
    }
}