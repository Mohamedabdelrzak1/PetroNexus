using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.DocumentRecords;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class DocumentRecordsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public DocumentRecordsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<DocumentRecordResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DocumentRecordService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<DocumentRecordResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DocumentRecordService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<DocumentRecordResponseDto>($"DocumentRecord #{id} not found.");
            return this.OkResponse(result);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<DocumentRecordResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DocumentRecordService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<DocumentRecordResponseDto>>> Create([FromBody] DocumentRecordCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DocumentRecordService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] DocumentRecordUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.DocumentRecordService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.DocumentRecordService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }
    }
}