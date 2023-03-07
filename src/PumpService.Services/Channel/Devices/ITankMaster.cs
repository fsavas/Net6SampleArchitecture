using PumpService.Services.Channel.Streams;

namespace PumpService.Services.Channel.Devices
{
    public interface ITankMaster : IDisposable
    {
        SerialTransport Transport { get; }
    }
}