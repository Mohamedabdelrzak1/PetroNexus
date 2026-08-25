using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.EscalationLogs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EscalationLogsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EscalationLogsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<EscalationLogResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EscalationLogService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<EscalationLogResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EscalationLogService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<EscalationLogResponseDto>($"EscalationLog #{id} not found.");
            return this.OkResponse(result);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<EscalationLogResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EscalationLogService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EscalationLogResponseDto>>> Create([FromBody] EscalationLogCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EscalationLogService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] EscalationLogUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.EscalationLogService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.EscalationLogService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }

        /// <summary>Acknowledges an escalation log.</summary>
        [HttpPost("{id:int}/acknowledge")]
        public async Task<ActionResult<ApiResponse>> Acknowledge(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.EscalationLogService.AcknowledgeAsync(id, cancellationToken);
            return this.OkResponse("Escalation acknowledged successfully.");
        }
    }
}
