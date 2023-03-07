using PumpService.Core;
using PumpService.Core.Domain.Stations;

namespace PumpService.Services.Stations
{
    public partial interface IStationService : IBaseService
    {
        void DeleteStation(long stationId);

        List<Station> GetAllStations();

        IPagedList<Station> SearchStations(StationSearch stationSearch);

        Station GetStationById(long stationId);

        void InsertStation(Station station);

        void UpdateStation(Station station);

        string ExportStations(StationSearch stationSearch);

        void Batch();
    }
}