using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using AutoMapper;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;

        public OfficesController(IOfficeService officeService, IMapper mapper)
        {
            _officeService = officeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offices = await _officeService.GetAllAsync();
            var officeDtos = _mapper.Map<List<OfficeResponseDto>>(offices);
            return Ok(officeDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var office = await _officeService.GetByIdAsync(id);
            if (office == null)
            {
                return NotFound();
            }
            var officeDto = _mapper.Map<OfficeResponseDto>(office);
            return Ok(officeDto);
        }
    }
}
