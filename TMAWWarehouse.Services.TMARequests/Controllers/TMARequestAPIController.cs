using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMAWarehouse.Services.TMARequest.Models.Dto;
using TMAWWarehouse.Services.TMARequests.Data;
using TMAWWarehouse.Services.TMARequests.Models;
using TMAWWarehouse.Services.TMARequests.Models.Dto;

namespace TMAWWarehouse.Services.TMARequests.Controllers
{
    [Route("Lists/Orders")]
    [ApiController]
    public class TMARequestAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;

        public TMARequestAPIController(AppDbContext db, IMapper mapper)
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
                IEnumerable<TMARequest> requestsList = await _db.TMARequests.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<TMARequestDto>>(requestsList);
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
                TMARequest request = await _db.TMARequests.FirstAsync(u => u.RequestID == id);
                _response.Result = _mapper.Map<TMARequestDto>(request);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("CreateTMARequest")]
        [Tags("Creators")]
        public async Task<ResponseDto?> Post([FromBody] TMARequestDto requestDto)
        {
            try
            {
                TMARequest item = _mapper.Map<TMARequest>(requestDto);
                _db.TMARequests.Add(item);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<TMARequestDto>(item);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("UpdateTMARequest")]
        [Tags("Updaters")]
        public async Task<ResponseDto?> Put([FromBody] TMARequestDto itemDto)
        {
            try
            {
                TMARequest item = _mapper.Map<TMARequest>(itemDto);
                _db.TMARequests.Update(item);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<TMARequestDto>(item);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeleteTMARequest/{id:int}")]
        [Tags("Deleters")]
        public async Task<ResponseDto?> Delete(int id)
        {
            try
            {
                TMARequest item = await _db.TMARequests.FirstAsync(u => u.RequestID == id);
                _db.TMARequests.Remove(item);
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
