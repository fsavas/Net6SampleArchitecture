using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.BackgroundJobs
{
    public partial class TaskScheduleSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}