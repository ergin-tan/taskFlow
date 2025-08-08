using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using AutoMapper;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTasksController : ControllerBase
    {
        private readonly IWorkTaskService _workTaskService;
        private readonly IMapper _mapper;

        public WorkTasksController(IWorkTaskService workTaskService, IMapper mapper)
        {
            _workTaskService = workTaskService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workTasks = await _workTaskService.GetAllAsync();
            var workTaskDtos = _mapper.Map<List<WorkTaskResponseDto>>(workTasks);
            return Ok(workTaskDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var workTask = await _workTaskService.GetByIdAsync(id);
            if (workTask == null)
            {
                return NotFound();
            }
            var workTaskDto = _mapper.Map<WorkTaskResponseDto>(workTask);
            return Ok(workTaskDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(WorkTaskRequestDto workTaskDto)
        {
            var workTask = _mapper.Map<WorkTask>(workTaskDto);
            await _workTaskService.AddAsync(workTask);
            var responseDto = _mapper.Map<WorkTaskResponseDto>(workTask);
            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkTaskRequestDto workTaskDto)
        {
            var existingWorkTask = await _workTaskService.GetByIdAsync(id);
            if (existingWorkTask == null)
            {
                return NotFound();
            }

            _mapper.Map(workTaskDto, existingWorkTask);
            existingWorkTask.UpdatedAt = DateTime.UtcNow;

            await _workTaskService.Update(existingWorkTask);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var workTask = await _workTaskService.GetByIdAsync(id);
            if (workTask == null)
            {
                return NotFound();
            }
            await _workTaskService.Remove(workTask);
            return NoContent();
        }
    }
}
