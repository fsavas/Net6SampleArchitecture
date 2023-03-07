namespace PumpService.Services.BackgroundJobs.ScopedTasks
{
    public interface IScopedProcessingService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}