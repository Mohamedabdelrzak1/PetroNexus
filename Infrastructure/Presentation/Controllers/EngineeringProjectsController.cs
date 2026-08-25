using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.EngineeringProjects;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EngineeringProjectsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EngineeringProjectsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<EngineeringProjectResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EngineeringProjectService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<EngineeringProjectResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EngineeringProjectService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<EngineeringProjectResponseDto>($"EngineeringProject #{id} not found.");
            return this.OkResponse(result);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<EngineeringProjectResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EngineeringProjectService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EngineeringProjectResponseDto>>> Create([FromBody] EngineeringProjectCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EngineeringProjectService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] EngineeringProjectUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.EngineeringProjectService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.EngineeringProjectService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }
    }
}