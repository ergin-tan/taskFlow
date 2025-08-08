using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using AutoMapper;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentsController : ControllerBase
    {
        private readonly ITaskAssignmentService _taskAssignmentService;
        private readonly IMapper _mapper;

        public TaskAssignmentsController(ITaskAssignmentService taskAssignmentService, IMapper mapper)
        {
            _taskAssignmentService = taskAssignmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var taskAssignments = await _taskAssignmentService.GetAllAsync();
            var taskAssignmentDtos = _mapper.Map<List<TaskAssignmentResponseDto>>(taskAssignments);
            return Ok(taskAssignmentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var taskAssignment = await _taskAssignmentService.GetByIdAsync(id);
            if (taskAssignment == null)
            {
                return NotFound();
            }
            var taskAssignmentDto = _mapper.Map<TaskAssignmentResponseDto>(taskAssignment);
            return Ok(taskAssignmentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskAssignmentRequestDto taskAssignmentDto)
        {
            var taskAssignment = _mapper.Map<TaskAssignment>(taskAssignmentDto);
            await _taskAssignmentService.AddAsync(taskAssignment);
            var responseDto = _mapper.Map<TaskAssignmentResponseDto>(taskAssignment);
            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskAssignmentRequestDto taskAssignmentDto)
        {
            var existingTaskAssignment = await _taskAssignmentService.GetByIdAsync(id);
            if (existingTaskAssignment == null)
            {
                return NotFound();
            }

            _mapper.Map(taskAssignmentDto, existingTaskAssignment);
            existingTaskAssignment.UpdatedAt = DateTime.UtcNow;

            await _taskAssignmentService.Update(existingTaskAssignment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var taskAssignment = await _taskAssignmentService.GetByIdAsync(id);
            if (taskAssignment == null)
            {
                return NotFound();
            }
            await _taskAssignmentService.Remove(taskAssignment);
            return NoContent();
        }
    }
}
