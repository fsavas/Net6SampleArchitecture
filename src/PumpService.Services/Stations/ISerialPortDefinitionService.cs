using PumpService.Core;
using PumpService.Core.Domain.Stations;

namespace PumpService.Services.Stations
{
    public partial interface ISerialPortDefinitionService : IBaseService
    {
        void DeleteSerialPortDefinition(long serialPortDefinitionId);

        List<SerialPortDefinition> GetAllSerialPortDefinitions();

        IPagedList<SerialPortDefinition> SearchSerialPortDefinitions(SerialPortDefinitionSearch serialPortDefinitionSearch);

        SerialPortDefinition GetSerialPortDefinitionById(long serialPortDefinitionId);

        void InsertSerialPortDefinition(SerialPortDefinition serialPortDefinition);

        void UpdateSerialPortDefinition(SerialPortDefinition serialPortDefinition);

        string ExportSerialPortDefinitions(SerialPortDefinitionSearch serialPortDefinitionSearch);
    }
}