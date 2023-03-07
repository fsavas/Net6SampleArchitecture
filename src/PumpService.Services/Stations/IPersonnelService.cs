using PumpService.Core;
using PumpService.Core.Domain.Stations;

namespace PumpService.Services.Stations
{
    public partial interface IPersonnelService : IBaseService
    {
        void DeletePersonnel(long personnelId);

        List<Personnel> GetAllPersonnels();

        IPagedList<Personnel> SearchPersonnels(PersonnelSearch personnelSearch);

        Personnel GetPersonnelById(long personnelId);

        void InsertPersonnel(Personnel personnel);

        void UpdatePersonnel(Personnel personnel);

        string ExportPersonnels(PersonnelSearch personnelSearch);
    }
}