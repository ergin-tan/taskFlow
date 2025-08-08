using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using AutoMapper;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskHistoriesController : ControllerBase
    {
        private readonly ITaskHistoryService _taskHistoryService;
        private readonly IMapper _mapper;

        public TaskHistoriesController(ITaskHistoryService taskHistoryService, IMapper mapper)
        {
            _taskHistoryService = taskHistoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var taskHistories = await _taskHistoryService.GetAllAsync();
            var taskHistoryDtos = _mapper.Map<List<TaskHistoryResponseDto>>(taskHistories);
            return Ok(taskHistoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var taskHistory = await _taskHistoryService.GetByIdAsync(id);
            if (taskHistory == null)
            {
                return NotFound();
            }
            var taskHistoryDto = _mapper.Map<TaskHistoryResponseDto>(taskHistory);
            return Ok(taskHistoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskHistoryRequestDto taskHistoryDto)
        {
            var taskHistory = _mapper.Map<TaskHistory>(taskHistoryDto);
            await _taskHistoryService.AddAsync(taskHistory);
            var responseDto = _mapper.Map<TaskHistoryResponseDto>(taskHistory);
            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskHistoryRequestDto taskHistoryDto)
        {
            var existingTaskHistory = await _taskHistoryService.GetByIdAsync(id);
            if (existingTaskHistory == null)
            {
                return NotFound();
            }

            _mapper.Map(taskHistoryDto, existingTaskHistory);
            existingTaskHistory.UpdatedAt = DateTime.UtcNow;

            await _taskHistoryService.Update(existingTaskHistory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var taskHistory = await _taskHistoryService.GetByIdAsync(id);
            if (taskHistory == null)
            {
                return NotFound();
            }
            await _taskHistoryService.Remove(taskHistory);
            return NoContent();
        }
    }
}
