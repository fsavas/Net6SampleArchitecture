namespace PumpService.Services.Channel.Pumps
{
    public partial interface IPumpService : IBaseService
    {
        bool UpdateUnitPrice(byte abuAddress, byte cpuId, Dictionary<byte, decimal>? nozzleIdPrices);

        decimal? GetNozzleTotalizer(byte abuAddress, byte cpuId, byte nozzleId, int? divide);
    }
}