using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMAWarehouse.Services.Item.Data;
using TMAWarehouse.Services.Item.Models.Dto;
using TMAWarehouse.Services.Item.Utility;

namespace TMAWarehouse.Services.Item.Controllers
{
    [Route("Lists/Items")]
    [Authorize]
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

        [HttpGet("GetAll")]
        [Tags("Getters")]
        public async Task<ResponseDto?> Get()
        {
            try
            {
                IEnumerable<Models.Item> itemList = await _db.Items.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<ItemDto>>(itemList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("Get/{id:int}")]
        [Tags("Getters")]
        public async Task<ResponseDto?> Get(int id)
        {
            try
            {
                Models.Item item = await _db.Items.FirstAsync(u => u.ItemID == id);
                _response.Result = _mapper.Map<ItemDto>(item);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("CreateItem")]
        [Tags("Creators")]
        [Authorize(Roles = SD.RoleAdmin + "," + SD.RoleCoordinator)]
        public async Task<ResponseDto?> Post([FromBody] ItemDto itemDto)
        {
            try
            {
                Models.Item item = _mapper.Map<Models.Item>(itemDto);
                _db.Items.Add(item);
                await _db.SaveChangesAsync();

                if(itemDto.Photo != null)
                {
                    string fileName = item.ItemID + Path.GetExtension(itemDto.Photo.FileName);
                    string filePach = @"wwwroot\ItemPhoto\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePach);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        itemDto.Photo.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    item.PhotoUrl = baseUrl + "/ItemPhoto/" + filePach;
                    item.PhotoLocalPach = filePach;
				}
                else
                {
                    item.PhotoUrl = "https://placehold.co/600x400";
                }

                _response.Result = _mapper.Map<ItemDto>(item);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("UpdateItem")]
        [Tags("Updaters")]
        [Authorize(Roles = SD.RoleAdmin + "," + SD.RoleCoordinator)]
        public async Task<ResponseDto?> Put([FromBody] ItemDto itemDto)
        {
            try
            {
                Models.Item item = _mapper.Map<Models.Item>(itemDto);
                _db.Items.Update(item);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<ItemDto>(item);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeleteItem/{id:int}")]
        [Tags("Deleters")]
        [Authorize(Roles = SD.RoleAdmin + "," + SD.RoleCoordinator)]
        public async Task<ResponseDto?> Delete(int id)
        {
            try
            {
                Models.Item item = await _db.Items.FirstAsync(u => u.ItemID == id);
                _db.Items.Remove(item);
                await _db.SaveChangesAsync();
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
