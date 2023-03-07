using Microsoft.EntityFrameworkCore;
using PumpService.Core;

namespace PumpService.Data
{
    public partial interface IDbContext
    {
        #region Methods

        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        int SaveChanges();

        #endregion Methods
    }
}