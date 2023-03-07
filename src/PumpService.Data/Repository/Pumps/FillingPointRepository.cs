using PumpService.Core;
using PumpService.Core.Domain.Pumps;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Pumps;

namespace PumpService.Data.Repository.Pumps
{
    public class FillingPointRepository : BaseRepository<FillingPoint>, IFillingPointRepository
    {
        #region Constructor

        public FillingPointRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<FillingPoint> SearchFillingPoints(FillingPointSearch fillingPointSearch)
        {
            var query = Table;
            AddQueryCriteria(query, fillingPointSearch);

            return new PagedList<FillingPoint>(query, fillingPointSearch.Page - 1, fillingPointSearch.PageSize);
        }

        public IList<FillingPoint> SearchAllFillingPoints(FillingPointSearch fillingPointSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, fillingPointSearch);

            return query.ToList();
        }

        public List<FillingPoint> GetAllFillingPoints()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<FillingPoint> AddQueryCriteria(IQueryable<FillingPoint> query, FillingPointSearch fillingPointSearch)
        {
            if (!string.IsNullOrEmpty(fillingPointSearch.Code))
                query = query.Where(s => s.Code.Contains(fillingPointSearch.Code));

            return LinqHelper<FillingPoint>.OrderBy(query, fillingPointSearch.OrderMember, fillingPointSearch.OrderByAsc);
        }

        #endregion Methods
    }
}