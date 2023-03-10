using PumpService.Core;

namespace PumpService.Services
{
    public partial class BaseService : IBaseService
    {
        #region Fields

        protected readonly IUnitOfWork _unitOfWork;

        #endregion Fields

        #region Constructor

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor
    }
}