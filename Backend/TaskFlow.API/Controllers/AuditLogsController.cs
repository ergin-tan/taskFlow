
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public AuditLogsController(IAuditLogService auditLogService, IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditLogResponseDto>>> GetAllAuditLogs()
        {
            var auditLogs = await _auditLogService.GetAllAsync();
            var auditLogDtos = _mapper.Map<IEnumerable<AuditLogResponseDto>>(auditLogs);
            return Ok(auditLogDtos);
        }
    }
}
