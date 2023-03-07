using AutoMapper;
using PumpService.Core.Domain.Stations;
using PumpService.Services.Enums;
using PumpService.Web.Core.Models.Stations;

namespace PumpService.Web.Core.Infrastructure.Mapper
{
    public class EnumMapper : IValueResolver<Personnel, PersonnelGridModel, string>
    {
        //private readonly IMemoryCache _memoryCache;
        private readonly IEnumManager _enumManager;

        public EnumMapper(IEnumManager enumManager)
        {
            //_memoryCache = memoryCache;
            _enumManager = enumManager;
        }

        public string Resolve(Personnel source, PersonnelGridModel destination, string destinationMember, ResolutionContext context)
        {
            return "aaa";
            //return _enumManager.GetDescription(source);
        }
    }
}