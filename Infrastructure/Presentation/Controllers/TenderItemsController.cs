using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.Tenders;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TenderItemsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TenderItemsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<TenderItemResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderItemService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<TenderItemResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderItemService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<TenderItemResponseDto>($"TenderItem #{id} not found.");
            return this.OkResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TenderItemResponseDto>>> Create([FromBody] TenderItemCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderItemService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] TenderItemUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.TenderItemService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.TenderItemService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }
    }
}