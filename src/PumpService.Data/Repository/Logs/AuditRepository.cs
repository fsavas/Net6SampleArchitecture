using PumpService.Core.Domain.Logs;
using PumpService.Core.Repository.Logs;

namespace PumpService.Data.Repository.Logs
{
    public class AuditRepository : BaseRepository<Audit>, IAuditRepository
    {
        #region Constructor

        public AuditRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor
    }
}