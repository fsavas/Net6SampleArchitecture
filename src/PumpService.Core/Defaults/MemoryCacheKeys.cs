namespace PumpService.Core.Defaults
{
    public static class MemoryCacheKeys
    {
        public const string User = "User";
        public const string Language = "Language";
        public const string Permissions = "Permissions";
        public const string KeySeperator = "-";
        public const string Sheet = "Sheet";
        public const string DashboardAccessToken = "DashboardAccessToken";
        public const string DashboardExpiration = "DashboardExpiration";
        
        #region UI Messages

        public const string NotLogin = "NotLogin";
        public const string UniqueIndexViolation = "UniqueIndexViolation";
        public const string UniqueConstraintViolation = "UniqueConstraintViolation";
        public const string ForeignKeyViolation = "ForeignKeyViolation";
        public const string CannotInsertTheValueNull = "CannotInsertTheValueNull";
        public const string ControllerActionSuccess = "ControllerActionSuccess";
        public const string InvalidUsernamePassword = "InvalidUsernamePassword";
        public const string LogoutUnsuccessful = "LogoutUnsuccessful";
        public const string PermissionDenied = "PermissionDenied";
        public const string SerialNumberCanBeAppliedToAsisProbe = "SerialNumberCanBeAppliedToAsisProbe";

        #endregion UI Messages

        #region Enums

        public const string EnumClasses_LookupTypes_AuditTypes_Insert = "EnumClasses_LookupTypes_AuditTypes_Insert";
        public const string EnumClasses_LookupTypes_AuditTypes_Update = "EnumClasses_LookupTypes_AuditTypes_Update";
        public const string EnumClasses_LookupTypes_AuditTypes_Delete = "EnumClasses_LookupTypes_AuditTypes_Delete";

        public const string EnumClasses_LookupTypes_ProbeTypes_Mepsan = "EnumClasses_LookupTypes_ProbeTypes_Mepsan";
        public const string EnumClasses_LookupTypes_ProbeTypes_Teosis = "EnumClasses_LookupTypes_ProbeTypes_Teosis";
        public const string EnumClasses_LookupTypes_ProbeTypes_Asis = "EnumClasses_LookupTypes_ProbeTypes_Asis";

        public const string EnumClasses_LookupTypes_MeasurementReasons_Periodic = "EnumClasses_LookupTypes_MeasurementReasons_Periodic";
        public const string EnumClasses_LookupTypes_MeasurementReasons_DuringPumpSales = "EnumClasses_LookupTypes_MeasurementReasons_DuringPumpSales";

        public const string EnumClasses_LookupTypes_DcrStates_IdleState = "EnumClasses_LookupTypes_DcrStates_IdleState";
        public const string EnumClasses_LookupTypes_DcrStates_CallState = "EnumClasses_LookupTypes_DcrStates_CallState";
        public const string EnumClasses_LookupTypes_DcrStates_BusyState = "EnumClasses_LookupTypes_DcrStates_BusyState";
        public const string EnumClasses_LookupTypes_DcrStates_PausedState = "EnumClasses_LookupTypes_DcrStates_PausedState";
        public const string EnumClasses_LookupTypes_DcrStates_UnpaidState = "EnumClasses_LookupTypes_DcrStates_UnpaidState";
        public const string EnumClasses_LookupTypes_DcrStates_ErrorState = "EnumClasses_LookupTypes_DcrStates_ErrorState";
        public const string EnumClasses_LookupTypes_DcrStates_AuthorizedState = "EnumClasses_LookupTypes_DcrStates_AuthorizedState";
        public const string EnumClasses_LookupTypes_DcrStates_WaitState = "EnumClasses_LookupTypes_DcrStates_WaitState";
        public const string EnumClasses_LookupTypes_DcrStates_PaidState = "EnumClasses_LookupTypes_DcrStates_PaidState";

        public const string EnumClasses_LookupTypes_MepsanStates_PumpNotProgrammedState = "EnumClasses_LookupTypes_MepsanStates_PumpNotProgrammedState";
        public const string EnumClasses_LookupTypes_MepsanStates_ResetState = "EnumClasses_LookupTypes_MepsanStates_ResetState";
        public const string EnumClasses_LookupTypes_MepsanStates_AuthorizedState = "EnumClasses_LookupTypes_MepsanStates_AuthorizedState";
        public const string EnumClasses_LookupTypes_MepsanStates_FillingState = "EnumClasses_LookupTypes_MepsanStates_FillingState";
        public const string EnumClasses_LookupTypes_MepsanStates_FillingCompletedState = "EnumClasses_LookupTypes_MepsanStates_FillingCompletedState";
        public const string EnumClasses_LookupTypes_MepsanStates_MaxAmountVolumeReachedState = "EnumClasses_LookupTypes_MepsanStates_MaxAmountVolumeReachedState";
        public const string EnumClasses_LookupTypes_MepsanStates_SwitchedOffState = "EnumClasses_LookupTypes_MepsanStates_SwitchedOffState";
        public const string EnumClasses_LookupTypes_MepsanStates_PausedState = "EnumClasses_LookupTypes_MepsanStates_PausedState";
        public const string EnumClasses_LookupTypes_MepsanStates_CallState = "EnumClasses_LookupTypes_MepsanStates_CallState";
        public const string EnumClasses_LookupTypes_MepsanStates_NeedTankStatusReadState = "EnumClasses_LookupTypes_MepsanStates_NeedTankStatusReadState";
        public const string EnumClasses_LookupTypes_MepsanStates_NeedAuthorizationState = "EnumClasses_LookupTypes_MepsanStates_NeedAuthorizationState";
        public const string EnumClasses_LookupTypes_MepsanStates_IdleState = "EnumClasses_LookupTypes_MepsanStates_IdleState";

        public const string EnumClasses_LookupTypes_DeviceTypeTypes_Serial = "EnumClasses_LookupTypes_DeviceTypeTypes_Serial";
        public const string EnumClasses_LookupTypes_DeviceTypeTypes_FillingPoint = "EnumClasses_LookupTypes_DeviceTypeTypes_FillingPoint";
        //public const string EnumClasses_LookupTypes_DeviceTypeTypes_Nozzle = "EnumClasses_LookupTypes_DeviceTypeTypes_Nozzle";

        public const string EnumClasses_LookupTypes_DeviceTypeGroups_Communication = "EnumClasses_LookupTypes_DeviceTypeGroups_Communication";
        public const string EnumClasses_LookupTypes_DeviceTypeGroups_Other = "EnumClasses_LookupTypes_DeviceTypeGroups_Other";

        public const string EnumClasses_LookupTypes_DeviceParameterTypes_Int = "EnumClasses_LookupTypes_DeviceParameterTypes_Int";
        public const string EnumClasses_LookupTypes_DeviceParameterTypes_String = "EnumClasses_LookupTypes_DeviceParameterTypes_String";

        public const string EnumClasses_LookupTypes_DeviceParameterNames_Com = "EnumClasses_LookupTypes_DeviceParameterNames_Com";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_BaudRate = "EnumClasses_LookupTypes_DeviceParameterNames_BaudRate";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_DataBits = "EnumClasses_LookupTypes_DeviceParameterNames_DataBits";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_StopBits = "EnumClasses_LookupTypes_DeviceParameterNames_StopBits";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_Parity = "EnumClasses_LookupTypes_DeviceParameterNames_Parity";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_ReadTimeout = "EnumClasses_LookupTypes_DeviceParameterNames_ReadTimeout";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_WriteTimeout = "EnumClasses_LookupTypes_DeviceParameterNames_WriteTimeout";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_AbuAddress = "EnumClasses_LookupTypes_DeviceParameterNames_AbuAddress";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_CpuId = "EnumClasses_LookupTypes_DeviceParameterNames_CpuId";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_NozzleId1 = "EnumClasses_LookupTypes_DeviceParameterNames_NozzleId1";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_NozzleId2 = "EnumClasses_LookupTypes_DeviceParameterNames_NozzleId2";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_NozzleId3 = "EnumClasses_LookupTypes_DeviceParameterNames_NozzleId3";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_NozzleId4 = "EnumClasses_LookupTypes_DeviceParameterNames_NozzleId4";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_NozzleId5 = "EnumClasses_LookupTypes_DeviceParameterNames_NozzleId5";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_NozzleId1_FuelType = "EnumClasses_LookupTypes_DeviceParameterNames_NozzleId1_FuelType";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_NozzleId2_FuelType = "EnumClasses_LookupTypes_DeviceParameterNames_NozzleId2_FuelType";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_NozzleId3_FuelType = "EnumClasses_LookupTypes_DeviceParameterNames_NozzleId3_FuelType";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_NozzleId4_FuelType = "EnumClasses_LookupTypes_DeviceParameterNames_NozzleId4_FuelType";
        public const string EnumClasses_LookupTypes_DeviceParameterNames_NozzleId5_FuelType = "EnumClasses_LookupTypes_DeviceParameterNames_NozzleId5_FuelType";

        #endregion Enums

        #region Models

        public const string PumpService_Web_Core_Models_Users_UserModel_Username_DisplayName = "PumpService_Web_Core_Models_Users_UserModel_Username_DisplayName";
        public const string PumpService_Web_Core_Models_Users_UserModel_Username_Required = "PumpService_Web_Core_Models_Users_UserModel_Username_Required";
        public const string PumpService_Web_Core_Models_Users_UserModel_Password_DisplayName = "PumpService_Web_Core_Models_Users_UserModel_Password_DisplayName";
        public const string PumpService_Web_Core_Models_Users_UserModel_Password_Required = "PumpService_Web_Core_Models_Users_UserModel_Password_Required";
        public const string PumpService_Web_Core_Models_Users_UserModel_Password_RegularExpression = "PumpService_Web_Core_Models_Users_UserModel_Password_RegularExpression";
        public const string PumpService_Web_Core_Models_Users_UserModel_AvailableRoles_DisplayName = "PumpService_Web_Core_Models_Users_UserModel_AvailableRoles_DisplayName";
        public const string PumpService_Web_Core_Models_Users_UserModel_SelectedRoles_Required = "PumpService_Web_Core_Models_Users_UserModel_SelectedRoles_Required";
        public const string PumpService_Web_Core_Models_Users_UserModel_SelectedRoles_DisplayName = "PumpService_Web_Core_Models_Users_UserModel_SelectedRoles_DisplayName";
        public const string PumpService_Web_Core_Models_Users_UserSearchModel_Username_DisplayName = "PumpService_Web_Core_Models_Users_UserSearchModel_Username_DisplayName";
        public const string PumpService_Web_Core_Models_Users_UserGridModel_Username_DisplayName = "PumpService_Web_Core_Models_Users_UserGridModel_Username_DisplayName";

        public const string PumpService_Web_Core_Models_Stations_StationModel_Name_DisplayName = "PumpService_Web_Core_Models_Stations_StationModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_StationModel_Name_Required = "PumpService_Web_Core_Models_Stations_StationModel_Name_Required";
        public const string PumpService_Web_Core_Models_Stations_StationSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_Stations_StationSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_StationGridModel_Name_DisplayName = "PumpService_Web_Core_Models_Stations_StationGridModel_Name_DisplayName";

        public const string PumpService_Web_Core_Models_Localizations_LanguageModel_Name_DisplayName = "PumpService_Web_Core_Models_Localizations_LanguageModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LanguageModel_Name_Required = "PumpService_Web_Core_Models_Localizations_LanguageModel_Name_Required";
        public const string PumpService_Web_Core_Models_Localizations_LanguageModel_Culture_DisplayName = "PumpService_Web_Core_Models_Localizations_LanguageModel_Culture_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LanguageModel_Culture_Required = "PumpService_Web_Core_Models_Localizations_LanguageModel_Culture_Required";
        public const string PumpService_Web_Core_Models_Localizations_LanguageSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_Localizations_LanguageSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LanguageGridModel_Name_DisplayName = "PumpService_Web_Core_Models_Localizations_LanguageGridModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LanguageGridModel_Culture_DisplayName = "PumpService_Web_Core_Models_Localizations_LanguageGridModel_Culture_DisplayName";

        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Name_DisplayName = "PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Name_Required = "PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Name_Required";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Value_DisplayName = "PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Value_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Value_Required = "PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Value_Required";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_DisplayName = "PumpService_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_Required = "PumpService_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_Required";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_Localizations_LocaleResourceSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceSearchModel_Value_DisplayName = "PumpService_Web_Core_Models_Localizations_LocaleResourceSearchModel_Value_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceSearchModel_LanguageId_DisplayName = "PumpService_Web_Core_Models_Localizations_LocaleResourceSearchModel_LanguageId_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceGridModel_Name_DisplayName = "PumpService_Web_Core_Models_Localizations_LocaleResourceGridModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceGridModel_Value_DisplayName = "PumpService_Web_Core_Models_Localizations_LocaleResourceGridModel_Value_DisplayName";
        public const string PumpService_Web_Core_Models_Localizations_LocaleResourceGridModel_Language_Name_DisplayName = "PumpService_Web_Core_Models_Localizations_LocaleResourceGridModel_Language_Name_DisplayName";

        public const string PumpService_Web_Core_Models_Security_RoleModel_Code_Required = "PumpService_Web_Core_Models_Security_RoleModel_Code_Required";
        public const string PumpService_Web_Core_Models_Security_RoleModel_Code_DisplayName = "PumpService_Web_Core_Models_Security_RoleModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Security_RoleModel_Name_Required = "PumpService_Web_Core_Models_Security_RoleModel_Name_Required";
        public const string PumpService_Web_Core_Models_Security_RoleModel_Name_DisplayName = "PumpService_Web_Core_Models_Security_RoleModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Security_RoleSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_Security_RoleSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Security_RoleGridModel_Code_DisplayName = "PumpService_Web_Core_Models_Security_RoleGridModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Security_RoleGridModel_Name_DisplayName = "PumpService_Web_Core_Models_Security_RoleGridModel_Name_DisplayName";

        public const string PumpService_Web_Core_Models_Security_PermissionModel_Code_Required = "PumpService_Web_Core_Models_Security_PermissionModel_Code_Required";
        public const string PumpService_Web_Core_Models_Security_PermissionModel_Code_DisplayName = "PumpService_Web_Core_Models_Security_PermissionModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Security_PermissionModel_Name_Required = "PumpService_Web_Core_Models_Security_PermissionModel_Name_Required";
        public const string PumpService_Web_Core_Models_Security_PermissionModel_Name_DisplayName = "PumpService_Web_Core_Models_Security_PermissionModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Security_PermissionSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_Security_PermissionSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Security_PermissionSearchModel_RoleId_DisplayName = "PumpService_Web_Core_Models_Security_PermissionSearchModel_RoleId_DisplayName";
        public const string PumpService_Web_Core_Models_Security_PermissionGridModel_Code_DisplayName = "PumpService_Web_Core_Models_Security_PermissionGridModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Security_PermissionGridModel_Name_DisplayName = "PumpService_Web_Core_Models_Security_PermissionGridModel_Name_DisplayName";

        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Code_Required = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Code_Required";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Code_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Name_Required = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Name_Required";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Name_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsActive_Required = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsActive_Required";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsActive_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnSuccess_Required = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnSuccess_Required";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnSuccess_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnSuccess_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnFailure_Required = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnFailure_Required";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnFailure_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnFailure_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryDelay_Required = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryDelay_Required";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryDelay_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryDelay_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryPeriod_Required = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryPeriod_Required";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryPeriod_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryPeriod_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsStopOnError_Required = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsStopOnError_Required";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsStopOnError_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsStopOnError_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_Code_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_Name_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsActive_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_NextRunOnSuccess_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_NextRunOnSuccess_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_NextRunOnFailure_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_NextRunOnFailure_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_EntryDelay_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_EntryDelay_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_EntryPeriod_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_EntryPeriod_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsStopOnError_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsStopOnError_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_LastStartTime_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_LastStartTime_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_LastEndTime_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_LastEndTime_DisplayName";
        public const string PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsRunning_DisplayName = "PumpService_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsRunning_DisplayName";

        public const string PumpService_Web_Core_Models_Products_ProductModel_Code_Required = "PumpService_Web_Core_Models_Products_ProductModel_Code_Required";
        public const string PumpService_Web_Core_Models_Products_ProductModel_Code_DisplayName = "PumpService_Web_Core_Models_Products_ProductModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductModel_Name_Required = "PumpService_Web_Core_Models_Products_ProductModel_Name_Required";
        public const string PumpService_Web_Core_Models_Products_ProductModel_Name_DisplayName = "PumpService_Web_Core_Models_Products_ProductModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductModel_UnitPrice_Required = "PumpService_Web_Core_Models_Products_ProductModel_UnitPrice_Required";
        public const string PumpService_Web_Core_Models_Products_ProductModel_UnitPrice_DisplayName = "PumpService_Web_Core_Models_Products_ProductModel_UnitPrice_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductModel_ProductGroupId_Required = "PumpService_Web_Core_Models_Products_ProductModel_ProductGroupId_Required";
        public const string PumpService_Web_Core_Models_Products_ProductModel_ProductGroupId_DisplayName = "PumpService_Web_Core_Models_Products_ProductModel_ProductGroupId_DisplayName";

        public const string PumpService_Web_Core_Models_Products_ProductModel_IsActive_Required = "PumpService_Web_Core_Models_Products_ProductModel_IsActive_Required";
        public const string PumpService_Web_Core_Models_Products_ProductModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Products_ProductModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_Products_ProductSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGridModel_Code_DisplayName = "PumpService_Web_Core_Models_Products_ProductGridModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGridModel_Name_DisplayName = "PumpService_Web_Core_Models_Products_ProductGridModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGridModel_UnitPrice_DisplayName = "PumpService_Web_Core_Models_Products_ProductGridModel_UnitPrice_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGridModel_ProductGroup_Name_DisplayName = "PumpService_Web_Core_Models_Products_ProductGridModel_ProductGroup_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGridModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Products_ProductGridModel_IsActive_DisplayName";

        public const string PumpService_Web_Core_Models_Pumps_FillingPointModel_Code_Required = "PumpService_Web_Core_Models_Pumps_FillingPointModel_Code_Required";
        public const string PumpService_Web_Core_Models_Pumps_FillingPointModel_Code_DisplayName = "PumpService_Web_Core_Models_Pumps_FillingPointModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_FillingPointModel_Address_Required = "PumpService_Web_Core_Models_Pumps_FillingPointModel_Address_Required";
        public const string PumpService_Web_Core_Models_Pumps_FillingPointModel_Address_DisplayName = "PumpService_Web_Core_Models_Pumps_FillingPointModel_Address_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_FillingPointModel_IsActive_Required = "PumpService_Web_Core_Models_Pumps_FillingPointModel_IsActive_Required";
        public const string PumpService_Web_Core_Models_Pumps_FillingPointModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Pumps_FillingPointModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_FillingPointSearchModel_Code_DisplayName = "PumpService_Web_Core_Models_Pumps_FillingPointSearchModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_FillingPointGridModel_Code_DisplayName = "PumpService_Web_Core_Models_Pumps_FillingPointGridModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_FillingPointGridModel_Address_DisplayName = "PumpService_Web_Core_Models_Pumps_FillingPointGridModel_Address_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_FillingPointGridModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Pumps_FillingPointGridModel_IsActive_DisplayName";

        public const string PumpService_Web_Core_Models_Pumps_NozzleModel_Address_Required = "PumpService_Web_Core_Models_Pumps_NozzleModel_Address_Required";
        public const string PumpService_Web_Core_Models_Pumps_NozzleModel_Address_DisplayName = "PumpService_Web_Core_Models_Pumps_NozzleModel_Address_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_NozzleModel_FillingPointId_Required = "PumpService_Web_Core_Models_Pumps_NozzleModel_FillingPointId_Required";
        public const string PumpService_Web_Core_Models_Pumps_NozzleModel_FillingPointId_DisplayName = "PumpService_Web_Core_Models_Pumps_NozzleModel_FillingPointId_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_NozzleModel_ProductId_Required = "PumpService_Web_Core_Models_Pumps_NozzleModel_ProductId_Required";
        public const string PumpService_Web_Core_Models_Pumps_NozzleModel_ProductId_DisplayName = "PumpService_Web_Core_Models_Pumps_NozzleModel_ProductId_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_NozzleModel_IsActive_Required = "PumpService_Web_Core_Models_Pumps_NozzleModel_IsActive_Required";
        public const string PumpService_Web_Core_Models_Pumps_NozzleModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Pumps_NozzleModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_NozzleSearchModel_ProductId_DisplayName = "PumpService_Web_Core_Models_Pumps_NozzleSearchModel_ProductId_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_NozzleGridModel_Address_DisplayName = "PumpService_Web_Core_Models_Pumps_NozzleGridModel_Address_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_NozzleGridModel_Product_Name_DisplayName = "PumpService_Web_Core_Models_Pumps_NozzleGridModel_Product_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_NozzleGridModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Pumps_NozzleGridModel_IsActive_DisplayName";

        public const string PumpService_Web_Core_Models_Products_ProductGroupModel_Code_Required = "PumpService_Web_Core_Models_Products_ProductGroupModel_Code_Required";
        public const string PumpService_Web_Core_Models_Products_ProductGroupModel_Code_DisplayName = "PumpService_Web_Core_Models_Products_ProductGroupModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGroupModel_Name_Required = "PumpService_Web_Core_Models_Products_ProductGroupModel_Name_Required";
        public const string PumpService_Web_Core_Models_Products_ProductGroupModel_Name_DisplayName = "PumpService_Web_Core_Models_Products_ProductGroupModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGroupModel_IsActive_Required = "PumpService_Web_Core_Models_Products_ProductGroupModel_IsActive_Required";
        public const string PumpService_Web_Core_Models_Products_ProductGroupModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Products_ProductGroupModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGroupSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_Products_ProductGroupSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGroupGridModel_Code_DisplayName = "PumpService_Web_Core_Models_Products_ProductGroupGridModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGroupGridModel_Name_DisplayName = "PumpService_Web_Core_Models_Products_ProductGroupGridModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Products_ProductGroupGridModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Products_ProductGroupGridModel_IsActive_DisplayName";

        public const string PumpService_Web_Core_Models_Pumps_PumpSalesModel_Amount_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesModel_Amount_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesModel_PumpQuantity_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesModel_PumpQuantity_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesModel_NetQuantity_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesModel_NetQuantity_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesModel_UnitPrice_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesModel_UnitPrice_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesModel_TransactionStartTime_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesModel_TransactionStartTime_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesModel_TransactionEndTime_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesModel_TransactionEndTime_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesModel_FillingPointId_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesModel_FillingPointId_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesModel_NozzleId_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesModel_NozzleId_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesModel_ProductId_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesModel_ProductId_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesSearchModel_TransactionStartTime_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesSearchModel_TransactionStartTime_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesSearchModel_TransactionEndTime_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesSearchModel_TransactionEndTime_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_Amount_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_Amount_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_PumpQuantity_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_PumpQuantity_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_NetQuantity_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_NetQuantity_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_UnitPrice_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_UnitPrice_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_TransactionStartTime_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_TransactionStartTime_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_TransactionEndTime_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_TransactionEndTime_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_FillingPoint_Code_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_FillingPoint_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_Nozzle_Address_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_Nozzle_Address_DisplayName";
        public const string PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_Product_Name_DisplayName = "PumpService_Web_Core_Models_Pumps_PumpSalesGridModel_Product_Name_DisplayName";

        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_PersonnelIdNumber_Required = "PumpService_Web_Core_Models_Stations_PersonnelModel_PersonnelIdNumber_Required";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_PersonnelIdNumber_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelModel_PersonnelIdNumber_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_Name_Required = "PumpService_Web_Core_Models_Stations_PersonnelModel_Name_Required";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_Name_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_PositionTypeId_Required = "PumpService_Web_Core_Models_Stations_PersonnelModel_PositionTypeId_Required";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_PositionTypeId_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelModel_PositionTypeId_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_DiscountRate_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelModel_DiscountRate_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_CardId_Required = "PumpService_Web_Core_Models_Stations_PersonnelModel_CardId_Required";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_CardId_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelModel_CardId_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_NationalIdNumber_Required = "PumpService_Web_Core_Models_Stations_PersonnelModel_NationalIdNumber_Required";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_NationalIdNumber_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelModel_NationalIdNumber_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_IsActive_Required = "PumpService_Web_Core_Models_Stations_PersonnelModel_IsActive_Required";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelModel_PositionTypes_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelModel_PositionTypes_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelGridModel_PersonnelIdNumber_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelGridModel_PersonnelIdNumber_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelGridModel_Name_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelGridModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelGridModel_PositionType_Description_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelGridModel_PositionType_Description_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelGridModel_DiscountRate_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelGridModel_DiscountRate_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelGridModel_CardId_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelGridModel_CardId_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelGridModel_NationalIdNumber_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelGridModel_NationalIdNumber_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_PersonnelGridModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Stations_PersonnelGridModel_IsActive_DisplayName";

        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortName_Required = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortName_Required";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortName_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortName_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_BaudRate_Required = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_BaudRate_Required";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_BaudRate_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_BaudRate_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_DataBits_Required = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_DataBits_Required";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_DataBits_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_DataBits_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_Parity_Required = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_Parity_Required";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_Parity_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_Parity_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_StopBits_Required = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_StopBits_Required";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_StopBits_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_StopBits_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_ReadTimeout_Required = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_ReadTimeout_Required";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_ReadTimeout_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_ReadTimeout_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_WriteTimeout_Required = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_WriteTimeout_Required";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_WriteTimeout_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_WriteTimeout_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortTypeId_Required = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortTypeId_Required";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortTypeId_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortTypeId_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_IsActive_Required = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_IsActive_Required";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortTypes_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionModel_PortTypes_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionSearchModel_PortName_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionSearchModel_PortName_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_PortName_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_PortName_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_BaudRate_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_BaudRate_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_DataBits_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_DataBits_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_Parity_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_Parity_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_StopBits_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_StopBits_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_ReadTimeout_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_ReadTimeout_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_WriteTimeout_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_WriteTimeout_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_PortType_Description_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_PortType_Description_DisplayName";
        public const string PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Stations_SerialPortDefinitionGridModel_IsActive_DisplayName";

        public const string PumpService_Web_Core_Models_Tanks_TankModel_Code_Required = "PumpService_Web_Core_Models_Tanks_TankModel_Code_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_Code_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddress_Required = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddress_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddress_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddress_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_SerialPortDefinitionId_Required = "PumpService_Web_Core_Models_Tanks_TankModel_SerialPortDefinitionId_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_SerialPortDefinitionId_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_SerialPortDefinitionId_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_Capacity_Required = "PumpService_Web_Core_Models_Tanks_TankModel_Capacity_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_Capacity_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_Capacity_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_Diameter_Required = "PumpService_Web_Core_Models_Tanks_TankModel_Diameter_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_Diameter_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_Diameter_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProductId_Required = "PumpService_Web_Core_Models_Tanks_TankModel_ProductId_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProductId_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_ProductId_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeTypeId_Required = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeTypeId_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeTypeId_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeTypeId_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeTypes_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeTypes_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_MeasurementPeriod_Required = "PumpService_Web_Core_Models_Tanks_TankModel_MeasurementPeriod_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_MeasurementPeriod_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_MeasurementPeriod_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeLength_Required = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeLength_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeLength_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeLength_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_TankGroupNo_Required = "PumpService_Web_Core_Models_Tanks_TankModel_TankGroupNo_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_TankGroupNo_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_TankGroupNo_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_GruptakiAktifTank_Required = "PumpService_Web_Core_Models_Tanks_TankModel_GruptakiAktifTank_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_GruptakiAktifTank_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_GruptakiAktifTank_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_LowFuelAlarm_Required = "PumpService_Web_Core_Models_Tanks_TankModel_LowFuelAlarm_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_LowFuelAlarm_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_LowFuelAlarm_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_HeightLimitBetweenTwoTankStatus_Required = "PumpService_Web_Core_Models_Tanks_TankModel_HeightLimitBetweenTwoTankStatus_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_HeightLimitBetweenTwoTankStatus_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_HeightLimitBetweenTwoTankStatus_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_IsDetectAutoFilling_Required = "PumpService_Web_Core_Models_Tanks_TankModel_IsDetectAutoFilling_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_IsDetectAutoFilling_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_IsDetectAutoFilling_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_WaterOffset_Required = "PumpService_Web_Core_Models_Tanks_TankModel_WaterOffset_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_WaterOffset_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_WaterOffset_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_FuelOffset_Required = "PumpService_Web_Core_Models_Tanks_TankModel_FuelOffset_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_FuelOffset_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_FuelOffset_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumber_Required = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumber_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumber_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumber_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumberApplied_Required = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumberApplied_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumberApplied_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeSerialNumberApplied_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddressAsis_Required = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddressAsis_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddressAsis_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_ProbeAddressAsis_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_IsActive_Required = "PumpService_Web_Core_Models_Tanks_TankModel_IsActive_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Tanks_TankModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankSearchModel_Code_DisplayName = "PumpService_Web_Core_Models_Tanks_TankSearchModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankGridModel_Code_DisplayName = "PumpService_Web_Core_Models_Tanks_TankGridModel_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankGridModel_ProbeAddress_DisplayName = "PumpService_Web_Core_Models_Tanks_TankGridModel_ProbeAddress_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankGridModel_SerialPortDefinitionPortName_DisplayName = "PumpService_Web_Core_Models_Tanks_TankGridModel_SerialPortDefinitionPortName_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankGridModel_Capacity_DisplayName = "PumpService_Web_Core_Models_Tanks_TankGridModel_Capacity_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankGridModel_Diameter_DisplayName = "PumpService_Web_Core_Models_Tanks_TankGridModel_Diameter_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankGridModel_Product_Name_DisplayName = "PumpService_Web_Core_Models_Tanks_TankGridModel_Product_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankGridModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Tanks_TankGridModel_IsActive_DisplayName";

        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelVolume_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelVolume_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelVolume_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelVolume_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelLength_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelLength_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelLength_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevelLength_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelVolume_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelVolume_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelVolume_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelVolume_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelLength_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelLength_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelLength_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_WaterLevelLength_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_StatusInfoDate_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_StatusInfoDate_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_StatusInfoDate_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_StatusInfoDate_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_TankId_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_TankId_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_TankId_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_TankId_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_IsActive_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_IsActive_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_OlcumSebebiId_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_OlcumSebebiId_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_OlcumSebebiId_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_OlcumSebebiId_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_OlcumSebebis_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_OlcumSebebis_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_SatisMiktari_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_SatisMiktari_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_SatisMiktari_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_SatisMiktari_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LT_Kalibrasyon_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LT_Kalibrasyon_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LT_Kalibrasyon_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LT_Kalibrasyon_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LTNet_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LTNet_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LTNet_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_FuelLevel_LTNet_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_PompaSatisId_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_PompaSatisId_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_PompaSatisId_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_PompaSatisId_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_TankDolumId_Required = "PumpService_Web_Core_Models_Tanks_TankStatusModel_TankDolumId_Required";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusModel_TankDolumId_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusModel_TankDolumId_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusSearchModel_TankId_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusSearchModel_TankId_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusGridModel_FuelLevelVolume_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusGridModel_FuelLevelVolume_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusGridModel_FuelLevelLength_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusGridModel_FuelLevelLength_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusGridModel_WaterLevelVolume_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusGridModel_WaterLevelVolume_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusGridModel_WaterLevelLength_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusGridModel_WaterLevelLength_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusGridModel_StatusInfoDate_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusGridModel_StatusInfoDate_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusGridModel_Tank_Code_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusGridModel_Tank_Code_DisplayName";
        public const string PumpService_Web_Core_Models_Tanks_TankStatusGridModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Tanks_TankStatusGridModel_IsActive_DisplayName";

        public const string PumpService_Web_Core_Models_Lookups_LookupTableModel_LookupType_Required = "PumpService_Web_Core_Models_Lookups_LookupTableModel_LookupType_Required";
        public const string PumpService_Web_Core_Models_Lookups_LookupTableModel_LookupType_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableModel_LookupType_DisplayName";
        public const string PumpService_Web_Core_Models_Lookups_LookupTableModel_Name_Required = "PumpService_Web_Core_Models_Lookups_LookupTableModel_Name_Required";
        public const string PumpService_Web_Core_Models_Lookups_LookupTableModel_Name_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableModel_Name_DisplayName";

        //public const string PumpService_Web_Core_Models_Lookups_LookupTableModel_Value_Required = "PumpService_Web_Core_Models_Lookups_LookupTableModel_Value_Required";
        //public const string PumpService_Web_Core_Models_Lookups_LookupTableModel_Value_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableModel_Value_DisplayName";
        public const string PumpService_Web_Core_Models_Lookups_LookupTableModel_Description_Required = "PumpService_Web_Core_Models_Lookups_LookupTableModel_Description_Required";

        public const string PumpService_Web_Core_Models_Lookups_LookupTableModel_Description_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableModel_Description_DisplayName";
        public const string PumpService_Web_Core_Models_Lookups_LookupTableModel_IsActive_Required = "PumpService_Web_Core_Models_Lookups_LookupTableModel_IsActive_Required";
        public const string PumpService_Web_Core_Models_Lookups_LookupTableModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableModel_IsActive_DisplayName";
        public const string PumpService_Web_Core_Models_Lookups_LookupTableSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Lookups_LookupTableGridModel_LookupType_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableGridModel_LookupType_DisplayName";
        public const string PumpService_Web_Core_Models_Lookups_LookupTableGridModel_Name_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableGridModel_Name_DisplayName";

        //public const string PumpService_Web_Core_Models_Lookups_LookupTableGridModel_Value_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableGridModel_Value_DisplayName";
        public const string PumpService_Web_Core_Models_Lookups_LookupTableGridModel_Description_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableGridModel_Description_DisplayName";

        public const string PumpService_Web_Core_Models_Lookups_LookupTableGridModel_IsActive_DisplayName = "PumpService_Web_Core_Models_Lookups_LookupTableGridModel_IsActive_DisplayName";

        public const string PumpService_Web_Core_Models_Devices_DeviceSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_Devices_DeviceSearchModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Devices_DeviceGridModel_Name_DisplayName = "PumpService_Web_Core_Models_Devices_DeviceGridModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Devices_DeviceModel_Name_DisplayName = "PumpService_Web_Core_Models_Devices_DeviceModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_Devices_DeviceModel_Name_Required = "PumpService_Web_Core_Models_Devices_DeviceModel_Name_Required";

        public const string PumpService_Web_Core_Models_DeviceParameters_DeviceParameterGridModel_Value_DisplayName = "PumpService_Web_Core_Models_DeviceParameters_DeviceParameterGridModel_Value_DisplayName";
        public const string PumpService_Web_Core_Models_DeviceParameters_DeviceParameterModel_Value_Required = "PumpService_Web_Core_Models_DeviceParameters_DeviceParameterModel_Value_Required";
        public const string PumpService_Web_Core_Models_DeviceParameters_DeviceParameterModel_Value_DisplayName = "PumpService_Web_Core_Models_DeviceParameters_DeviceParameterModel_Value_DisplayName";
        public const string PumpService_Web_Core_Models_DeviceParameters_DeviceParameterSearchModel_Value_DisplayName = "PumpService_Web_Core_Models_DeviceParameters_DeviceParameterSearchModel_Value_DisplayName";
        public const string PumpService_Web_Core_Models_DeviceParameters_DeviceParameterGridModel_Name_DisplayName = "PumpService_Web_Core_Models_DeviceParameters_DeviceParameterGridModel_Name_DisplayName";

        public const string PumpService_Web_Core_Models_DeviceTypes_DeviceTypeGridModel_Name_DisplayName = "PumpService_Web_Core_Models_DeviceTypes_DeviceTypeGridModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_DeviceTypes_DeviceTypeModel_Name_Required = "PumpService_Web_Core_Models_DeviceTypes_DeviceTypeModel_Name_Required";
        public const string PumpService_Web_Core_Models_DeviceTypes_DeviceTypeModel_Name_DisplayName = "PumpService_Web_Core_Models_DeviceTypes_DeviceTypeModel_Name_DisplayName";
        public const string PumpService_Web_Core_Models_DeviceTypes_DeviceTypeSearchModel_Name_DisplayName = "PumpService_Web_Core_Models_DeviceTypes_DeviceTypeSearchModel_Name_DisplayName";

        #endregion Models
    }
}