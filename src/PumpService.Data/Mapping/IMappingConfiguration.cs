using Microsoft.EntityFrameworkCore;

namespace PumpService.Data.Mapping
{
    public partial interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}