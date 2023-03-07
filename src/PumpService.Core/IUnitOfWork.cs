using PumpService.Core.Repository;

namespace PumpService.Core
{
    public interface IUnitOfWork
    {
        #region Methods

        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        int SaveChanges(bool isTask = false);

        #endregion Methods
    }
}