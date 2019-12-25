using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GoseiVn.DemoApp.Estimates.Dto;

namespace GoseiVn.DemoApp
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateEstimateDto, Models.Estimates>();
            configuration.CreateMap<CreateImageDto, Models.Images>();
        }
    }
}
