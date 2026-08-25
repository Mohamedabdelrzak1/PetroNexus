using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.VendorRegistrations;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class VendorRegistrationsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public VendorRegistrationsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<VendorRegistrationResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.VendorRegistrationService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<VendorRegistrationResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.VendorRegistrationService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<VendorRegistrationResponseDto>($"VendorRegistration #{id} not found.");
            return this.OkResponse(result);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<VendorRegistrationResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.VendorRegistrationService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<VendorRegistrationResponseDto>>>> Summary(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.VendorRegistrationService.GetSummaryAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<VendorRegistrationResponseDto>>> Create([FromBody] VendorRegistrationCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.VendorRegistrationService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] VendorRegistrationUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.VendorRegistrationService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.VendorRegistrationService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }
    }
}