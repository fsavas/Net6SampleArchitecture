using AutoMapper;
using PumpService.Core;
using PumpService.Core.Domain.BackgroundJobs;
using PumpService.Core.Domain.Devices;
using PumpService.Core.Domain.Localizations;
using PumpService.Core.Domain.Lookups;
using PumpService.Core.Domain.Products;
using PumpService.Core.Domain.Pumps;
using PumpService.Core.Domain.Security;
using PumpService.Core.Domain.Stations;
using PumpService.Core.Domain.Tanks;
using PumpService.Core.Domain.Users;
using PumpService.Core.Helpers;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.BackgroundJobs;
using PumpService.Web.Core.Models.Devices;
using PumpService.Web.Core.Models.Localizations;
using PumpService.Web.Core.Models.Lookups;
using PumpService.Web.Core.Models.Products;
using PumpService.Web.Core.Models.Pumps;
using PumpService.Web.Core.Models.Security;
using PumpService.Web.Core.Models.Stations;
using PumpService.Web.Core.Models.Tanks;
using PumpService.Web.Core.Models.Users;

namespace PumpService.Web.Infrastructure.Mapper
{
    public class MapperConfiguration : Profile
    {
        #region Constructor

        public MapperConfiguration()
        {
            //create all of the maps
            CreateMaps();
        }

        #endregion Constructor

        #region Utilities

        private void CreateMaps()
        {
            CreateMap<Station, StationModel>();
            CreateMap<StationModel, Station>();
            CreateMap<StationSearchModel, StationSearch>();
            CreateMap<Station, StationGrid>();
            CreateMap<Station, StationGridModel>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<UserSearchModel, UserSearch>();
            CreateMap<User, UserGrid>();
            CreateMap<User, UserGridModel>();

            CreateMap<Language, LanguageModel>();
            CreateMap<LanguageModel, Language>();
            CreateMap<LanguageSearchModel, LanguageSearch>();
            CreateMap<Language, LanguageGrid>();
            CreateMap<Language, LanguageGridModel>();

            CreateMap<LocaleResource, LocaleResourceModel>();
            CreateMap<LocaleResourceModel, LocaleResource>();
            CreateMap<LocaleResourceSearchModel, LocaleResourceSearch>();
            CreateMap<LocaleResource, LocaleResourceGrid>()
                .ForMember(dto => dto.Language_Name, conf => conf.MapFrom(e => e.Language.Name));
            CreateMap<LocaleResource, LocaleResourceGridModel>()
                .ForMember(dto => dto.Language_Name, conf => conf.MapFrom(e => e.Language.Name));

            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();
            CreateMap<RoleSearchModel, RoleSearch>();
            CreateMap<Role, RoleGrid>();
            CreateMap<Role, RoleGridModel>();

            CreateMap<Permission, PermissionModel>();
            CreateMap<PermissionModel, Permission>();
            CreateMap<PermissionSearchModel, PermissionSearch>();
            CreateMap<Permission, PermissionGrid>();
            CreateMap<Permission, PermissionGridModel>();

            CreateMap<TaskSchedule, TaskScheduleModel>();
            CreateMap<TaskScheduleModel, TaskSchedule>();
            CreateMap<TaskScheduleSearchModel, TaskScheduleSearch>();
            CreateMap<TaskSchedule, TaskScheduleGrid>();
            CreateMap<TaskSchedule, TaskScheduleGridModel>();

            CreateMap<FillingPoint, FillingPointModel>();
            CreateMap<FillingPointModel, FillingPoint>();
            CreateMap<FillingPointSearchModel, FillingPointSearch>();
            CreateMap<FillingPoint, FillingPointGrid>();
            CreateMap<FillingPoint, FillingPointGridModel>();

            CreateMap<Nozzle, NozzleModel>();
            CreateMap<NozzleModel, Nozzle>();
            CreateMap<NozzleSearchModel, NozzleSearch>();
            CreateMap<Nozzle, NozzleGrid>()
                .ForMember(dto => dto.Product_Name, conf => conf.MapFrom(e => e.Product.Name));
            CreateMap<Nozzle, NozzleGridModel>()
                .ForMember(dto => dto.Product_Name, conf => conf.MapFrom(e => e.Product.Name));

            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
            CreateMap<ProductSearchModel, ProductSearch>();
            CreateMap<Product, ProductGrid>()
                .ForMember(dto => dto.ProductGroup_Name, conf => conf.MapFrom(e => e.ProductGroup.Name));
            CreateMap<Product, ProductGridModel>()
                .ForMember(dto => dto.ProductGroup_Name, conf => conf.MapFrom(e => e.ProductGroup.Name));

            CreateMap<ProductGroup, ProductGroupModel>();
            CreateMap<ProductGroupModel, ProductGroup>();
            CreateMap<ProductGroupSearchModel, ProductGroupSearch>();
            CreateMap<ProductGroup, ProductGroupGrid>();
            CreateMap<ProductGroup, ProductGroupGridModel>();

            CreateMap<PumpSales, PumpSalesModel>();
            CreateMap<PumpSalesModel, PumpSales>();
            CreateMap<PumpSalesSearchModel, PumpSalesSearch>();
            CreateMap<PumpSales, PumpSalesGrid>()
                .ForMember(dto => dto.FillingPoint_Code, conf => conf.MapFrom(e => e.FillingPoint.Code))
                .ForMember(dto => dto.Nozzle_Address, conf => conf.MapFrom(e => e.Nozzle.Address))
                .ForMember(dto => dto.Product_Name, conf => conf.MapFrom(e => e.Product.Name));
            CreateMap<PumpSales, PumpSalesGridModel>()
                .ForMember(dto => dto.FillingPoint_Code, conf => conf.MapFrom(e => e.FillingPoint.Code))
                .ForMember(dto => dto.Nozzle_Address, conf => conf.MapFrom(e => e.Nozzle.Address))
                .ForMember(dto => dto.Product_Name, conf => conf.MapFrom(e => e.Product.Name));

            //todo enums
            CreateMap<SerialPortDefinition, SerialPortDefinitionModel>();
            CreateMap<SerialPortDefinitionModel, SerialPortDefinition>();
            CreateMap<SerialPortDefinitionSearchModel, SerialPortDefinitionSearch>();
            CreateMap<SerialPortDefinition, SerialPortDefinitionGrid>();
            CreateMap<SerialPortDefinition, SerialPortDefinitionGridModel>()
                .ForMember(dto => dto.PortType_Description, conf => conf.MapFrom(e => e.PortType.Description))
                .ForMember(dto => dto.Parity, conf => conf.MapFrom(e => EnumHelper.GetDescription(e.Parity)))
                .ForMember(dto => dto.StopBits, conf => conf.MapFrom(e => EnumHelper.GetDescription(e.StopBits)));

            //todo enums
            CreateMap<Personnel, PersonnelModel>();
            CreateMap<PersonnelModel, Personnel>();
            CreateMap<PersonnelSearchModel, PersonnelSearch>();
            CreateMap<Personnel, PersonnelGrid>();
            CreateMap<Personnel, PersonnelGridModel>()
                .ForMember(dto => dto.PositionType_Description, conf => conf.MapFrom(e => e.PositionType.Description));

            CreateMap<Tank, TankModel>();
            CreateMap<TankModel, Tank>();
            CreateMap<TankSearchModel, TankSearch>();
            CreateMap<Tank, TankGrid>()
                .ForMember(dto => dto.SerialPortDefinitionPortName, conf => conf.MapFrom(e => e.SerialPortDefinition.PortName))
                .ForMember(dto => dto.Product_Name, conf => conf.MapFrom(e => e.Product.Name));
            CreateMap<Tank, TankGridModel>()
                .ForMember(dto => dto.SerialPortDefinitionPortName, conf => conf.MapFrom(e => e.SerialPortDefinition.PortName))
                .ForMember(dto => dto.Product_Name, conf => conf.MapFrom(e => e.Product.Name));

            CreateMap<TankStatus, TankStatusModel>();
            CreateMap<TankStatusModel, TankStatus>();
            CreateMap<TankStatusSearchModel, TankStatusSearch>();
            CreateMap<TankStatus, TankStatusGrid>()
                .ForMember(dto => dto.Tank_Code, conf => conf.MapFrom(e => e.Tank.Code));
            CreateMap<TankStatus, TankStatusGridModel>()
                .ForMember(dto => dto.Tank_Code, conf => conf.MapFrom(e => e.Tank.Code));

            CreateMap<LookupTable, LookupTableModel>();
            CreateMap<LookupTableModel, LookupTable>();
            CreateMap<LookupTableSearchModel, LookupTableSearch>();
            CreateMap<LookupTable, LookupTableGrid>();
            CreateMap<LookupTable, LookupTableGridModel>();

            CreateMap<Device, DeviceModel>();
            CreateMap<DeviceModel, Device>();
            CreateMap<DeviceSearchModel, DeviceSearch>();
            CreateMap<Device, DeviceGrid>();
            CreateMap<Device, DeviceGridModel>();

            CreateMap<DeviceParameter, DeviceParameterModel>();
            CreateMap<DeviceParameterModel, DeviceParameter>();
            CreateMap<DeviceParameterSearchModel, DeviceParameterSearch>();
            CreateMap<DeviceParameter, DeviceParameterGrid>();
            CreateMap<DeviceParameter, DeviceParameterGridModel>();

            CreateMap<DeviceType, DeviceTypeModel>();
            CreateMap<DeviceTypeModel, DeviceType>();
            CreateMap<DeviceTypeSearchModel, DeviceTypeSearch>();
            CreateMap<DeviceType, DeviceTypeGrid>();
            CreateMap<DeviceType, DeviceTypeGridModel>();

            CreateMap<SelectListItem, SelectListItemModel>();
            CreateMap<SelectListItemModel, SelectListItem>();
            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListMapper<,>));
        }

        #endregion Utilities
    }
}