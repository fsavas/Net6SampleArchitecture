using PumpService.Core;
using PumpService.Core.Domain.BackgroundJobs;

namespace PumpService.Services.BackgroundJobs
{
    public partial interface ITaskScheduleService : IBaseService
    {
        void DeleteTaskSchedule(long taskScheduleId);

        List<TaskSchedule> GetAllTaskSchedules();

        IPagedList<TaskSchedule> SearchTaskSchedules(TaskScheduleSearch taskScheduleSearch);

        TaskSchedule GetTaskScheduleById(long taskScheduleId);

        void InsertTaskSchedule(TaskSchedule taskSchedule);

        void UpdateTaskSchedule(TaskSchedule taskSchedule);

        string ExportTaskSchedules(TaskScheduleSearch taskScheduleSearch);
    }
}