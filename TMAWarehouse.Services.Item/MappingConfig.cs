using AutoMapper;
using TMAWarehouse.Services.Item.Models.Dto;
using TMAWarehouse.Services.Item.Models;

namespace TMAWarehouse.Services.Item
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ItemDto, Models.Item>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
