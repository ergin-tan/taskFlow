using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using AutoMapper;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var userDtos = _mapper.Map<List<UserResponseDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<UserResponseDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserRequestDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userService.AddAsync(user);
            var responseDto = _mapper.Map<UserResponseDto>(user);
            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequestDto userDto)
        {
            var existingUser = await _userService.GetByIdAsync(userDto.Id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _mapper.Map(userDto, existingUser);
            
            existingUser.UpdatedAt = DateTime.UtcNow;

            await _userService.Update(existingUser);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.Remove(user);
            return NoContent();
        }
    }
}