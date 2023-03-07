using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Pumps
{
    public partial class FillingPointSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_FillingPointSearchModel_Code_DisplayName)]
        public string Code { get; set; }

        #endregion Properties
    }
}