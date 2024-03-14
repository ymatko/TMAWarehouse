using AutoMapper;
using TMAWWarehouse.Services.TMARequests.Models;
using TMAWWarehouse.Services.TMARequests.Models.Dto;

namespace TMAWWarehouse.Services.TMARequests
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<TMARequest, TMARequestDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
