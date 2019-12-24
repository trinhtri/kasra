using AutoMapper;
using GoseiVn.DemoApp.Estimates.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateImageDto, Models.Images>().ReverseMap();
            configuration.CreateMap<CreateEstimateDto, Models.Estimates>().ReverseMap();
        }
    }
}
