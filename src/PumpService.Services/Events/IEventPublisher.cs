namespace PumpService.Services.Events
{
    public partial interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event);
    }
}