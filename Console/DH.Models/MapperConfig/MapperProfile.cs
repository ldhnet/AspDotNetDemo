using AutoMapper;
using DH.Models.DbModels;
using Framework.Auth;
using Framework.Utility.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.Models.MapperConfig
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, OperatorInfo>();
            CreateMap<OperatorInfo, Employee>();

            //其他配置：驼峰命名与Pascal命名的兼容
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
        }
    }
}
