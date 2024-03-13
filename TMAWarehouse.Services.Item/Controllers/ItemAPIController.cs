using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMAWarehouse.Services.Item.Data;
using TMAWarehouse.Services.Item.Models.Dto;

namespace TMAWarehouse.Services.Item.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;

        public ItemAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Tags("Getters")]
        public ResponseDto? Get()
        {
            try
            {
                IEnumerable<Models.Item> itemList = _db.Items.ToList();
                _response.Result = _mapper.Map<IEnumerable<ItemDto>>(itemList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
