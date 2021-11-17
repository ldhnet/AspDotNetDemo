using Framework.Utility.Config;
using LangResources;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Framework.Utility
{
    public static class ShardResource
    { 
        private static IStringLocalizer<LangResource> _localizer;
        public static void UseShardResource(this IApplicationBuilder applicationBuilder)
        {  
            _localizer = applicationBuilder.ApplicationServices.GetRequiredService<IStringLocalizer<LangResource>>();
        }

        public static string Hello => _localizer.GetString(nameof(Hello));
        public static string Title => _localizer.GetString(nameof(Title));
        public static string CheckNotNull => _localizer.GetString(nameof(CheckNotNull));
        public static string AnyRadixConvert_CharacterIsNotValid => _localizer.GetString(nameof(AnyRadixConvert_CharacterIsNotValid));
        public static string AnyRadixConvert_Overflow => _localizer.GetString(nameof(AnyRadixConvert_Overflow));
        public static string Caching_CacheNotInitialized => _localizer.GetString(nameof(Caching_CacheNotInitialized));
        public static string ConfigFile_ItemKeyDefineRepeated => _localizer.GetString(nameof(ConfigFile_ItemKeyDefineRepeated));
        public static string ConfigFile_NameToTypeIsNull => _localizer.GetString(nameof(ConfigFile_NameToTypeIsNull));
        public static string Context_BuildServicesFirst => _localizer.GetString(nameof(Context_BuildServicesFirst));
        public static string DbContextInitializerConfig_InitializerNotExists => _localizer.GetString(nameof(DbContextInitializerConfig_InitializerNotExists));
        public static string Filter_GroupOperateError => _localizer.GetString(nameof(Filter_GroupOperateError));
        public static string Filter_RuleFieldInTypeNotFound => _localizer.GetString(nameof(Filter_RuleFieldInTypeNotFound));
        public static string Http_Seciruty_Client_DecryptResponse_Failt => _localizer.GetString(nameof(Http_Seciruty_Client_DecryptResponse_Failt));
        public static string Http_Security_Client_EncryptRequest_Failt => _localizer.GetString(nameof(Http_Security_Client_EncryptRequest_Failt));
        public static string stringHttp_Security_Client_VerifyResponse_Failt => _localizer.GetString(nameof(stringHttp_Security_Client_VerifyResponse_Failt));
        public static string Http_Security_Host_DecryptRequest_Failt => _localizer.GetString(nameof(Http_Security_Host_DecryptRequest_Failt));
        public static string Http_Security_Host_EncryptResponse_Failt => _localizer.GetString(nameof(Http_Security_Host_EncryptResponse_Failt));
        public static string Ioc_CannotResolveService => _localizer.GetString(nameof(Ioc_CannotResolveService));
        public static string Ioc_FrameworkNotInitialized => _localizer.GetString(nameof(Ioc_FrameworkNotInitialized));
        public static string Ioc_ImplementationTypeNotFound => _localizer.GetString(nameof(Ioc_ImplementationTypeNotFound));
        public static string Ioc_NoConstructorMatch => _localizer.GetString(nameof(Ioc_NoConstructorMatch));
        public static string Ioc_TryAddIndistinguishableTypeToEnumerable => _localizer.GetString(nameof(Ioc_TryAddIndistinguishableTypeToEnumerable));
        public static string IocInitializerBase_TypeNotIRepositoryType => _localizer.GetString(nameof(IocInitializerBase_TypeNotIRepositoryType));
        public static string IocInitializerBase_TypeNotIUnitOfWorkType => _localizer.GetString(nameof(IocInitializerBase_TypeNotIUnitOfWorkType));
        public static string Logging_CreateLogInstanceReturnNull => _localizer.GetString(nameof(Logging_CreateLogInstanceReturnNull));
        public static string Map_MapperIsNull => _localizer.GetString(nameof(Map_MapperIsNull));
        public static string Mef_HttpContextItems_NotFoundRequestContainer => _localizer.GetString(nameof(Mef_HttpContextItems_NotFoundRequestContainer));
        public static string ObjectExtensions_PropertyNameNotExistsInType => _localizer.GetString(nameof(ObjectExtensions_PropertyNameNotExistsInType));
        public static string ObjectExtensions_PropertyNameNotFixedType => _localizer.GetString(nameof(ObjectExtensions_PropertyNameNotFixedType));
        public static string ParameterCheck_Between => _localizer.GetString(nameof(ParameterCheck_Between));
        public static string ParameterCheck_BetweenNotEqual => _localizer.GetString(nameof(ParameterCheck_BetweenNotEqual));
        public static string ParameterCheck_DirectoryNotExists => _localizer.GetString(nameof(ParameterCheck_DirectoryNotExists));
        public static string ParameterCheck_FileNotExists => _localizer.GetString(nameof(ParameterCheck_FileNotExists));
        public static string ParameterCheck_NotContainsNull_Collection => _localizer.GetString(nameof(ParameterCheck_NotContainsNull_Collection));
        public static string ParameterCheck_NotEmpty_Guid => _localizer.GetString(nameof(ParameterCheck_NotEmpty_Guid));
        public static string ParameterCheck_NotGreaterThan => _localizer.GetString(nameof(ParameterCheck_NotGreaterThan));
        public static string ParameterCheck_NotGreaterThanOrEqual => _localizer.GetString(nameof(ParameterCheck_NotGreaterThanOrEqual));
        public static string ParameterCheck_NotLessThan => _localizer.GetString(nameof(ParameterCheck_NotLessThan));
        public static string ParameterCheck_NotLessThanOrEqual => _localizer.GetString(nameof(ParameterCheck_NotLessThanOrEqual));
        public static string ParameterCheck_NotNull => _localizer.GetString(nameof(ParameterCheck_NotNull));
        public static string ParameterCheck_NotNullOrEmpty_Collection => _localizer.GetString(nameof(ParameterCheck_NotNullOrEmpty_Collection));
        public static string ParameterCheck_NotNullOrEmpty_String => _localizer.GetString(nameof(ParameterCheck_NotNullOrEmpty_String));
    }
}