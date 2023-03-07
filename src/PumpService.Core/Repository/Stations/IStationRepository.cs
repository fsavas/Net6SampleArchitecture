using PumpService.Core.Domain.Stations;

namespace PumpService.Core.Repository.Stations
{
    public partial interface IStationRepository : IBaseRepository<Station>
    {
        #region Methods

        IPagedList<Station> SearchStations(StationSearch stationSearch);

        List<Station> GetAllStations();

        IList<Station> SearchAllStations(StationSearch stationSearch);

        void Batch();

        #endregion Methods
    }
}