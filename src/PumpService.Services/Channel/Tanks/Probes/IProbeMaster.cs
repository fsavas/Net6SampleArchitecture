using PumpService.Core.Domain.Lookups;

namespace PumpService.Services.Channel.Tanks.Probes
{
    public interface IProbeMaster : IDisposable
    {
        int QueryProbe(LookupTable tankMeasurementReason);

        bool FindAndSetProbeSerialNumber();

        bool SetAddressOnProbe(int newAddress);
    }
}