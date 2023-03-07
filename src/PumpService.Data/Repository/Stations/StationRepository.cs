using PumpService.Core;
using PumpService.Core.Domain.Stations;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Stations;

namespace PumpService.Data.Repository.Stations
{
    public class StationRepository : BaseRepository<Station>, IStationRepository
    {
        public StationRepository(IDbContext context)
            : base(context)
        {
        }

        public IPagedList<Station> SearchStations(StationSearch stationSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, stationSearch);

            return new PagedList<Station>(query, stationSearch.Page - 1, stationSearch.PageSize);
        }

        public IList<Station> SearchAllStations(StationSearch stationSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, stationSearch);

            return query.ToList();
        }

        public List<Station> GetAllStations()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<Station> AddQueryCriteria(IQueryable<Station> query, StationSearch stationSearch)
        {
            if (!string.IsNullOrEmpty(stationSearch.Name))
                query = query.Where(s => s.Name.Contains(stationSearch.Name));

            return LinqHelper<Station>.OrderBy(query, stationSearch.OrderMember, stationSearch.OrderByAsc);
        }

        public void Batch()
        {
            //var x = 1;
            //if (_context is DbContext dbContext)
            //{
            //    using (var transaction = dbContext.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            Entities.Add(new Station());
            //            _context.SaveChanges();

            //            Entities.Add(new Station());
            //            _context.SaveChanges();

            //            var entity = _context.Set<Station>();
            //            var station = from s in Table orderby s.Id select s;

            //            transaction.Commit();
            //        }
            //        catch (Exception)
            //        {
            //            transaction.Rollback();
            //        }
            //    }
            //}
        }
    }
}