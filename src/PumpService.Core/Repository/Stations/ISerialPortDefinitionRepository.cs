using PumpService.Core.Domain.Stations;

namespace PumpService.Core.Repository.Stations
{
    public partial interface ISerialPortDefinitionRepository : IBaseRepository<SerialPortDefinition>
    {
        #region Methods

        IPagedList<SerialPortDefinition> SearchSerialPortDefinitions(SerialPortDefinitionSearch serialPortDefinitionSearch);

        List<SerialPortDefinition> GetAllSerialPortDefinitions();

        IList<SerialPortDefinition> SearchAllSerialPortDefinitions(SerialPortDefinitionSearch serialPortDefinitionSearch);

        #endregion Methods
    }
}