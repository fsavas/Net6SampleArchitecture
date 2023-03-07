using PumpService.Core.Domain.BackgroundJobs;

namespace PumpService.Core.Repository.BackgroundJobs
{
    public partial interface ITaskScheduleRepository : IBaseRepository<TaskSchedule>
    {
        #region Methods

        IPagedList<TaskSchedule> SearchTaskSchedules(TaskScheduleSearch taskScheduleSearch);

        List<TaskSchedule> GetAllTaskSchedules();

        IList<TaskSchedule> SearchAllTaskSchedules(TaskScheduleSearch taskScheduleSearch);

        TaskSchedule GetByCode(string code);

        #endregion Methods
    }
}