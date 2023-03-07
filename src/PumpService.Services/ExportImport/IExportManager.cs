using PumpService.Core;

namespace PumpService.Services.ExportImport
{
    public partial interface IExportManager<TModel, TEntity> where TModel : class where TEntity : BaseEntity
    {
        string ExportToExcel(List<TEntity> list);
    }
}