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
    public class TenderLeadsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TenderLeadsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<TenderLeadResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderLeadService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<TenderLeadResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderLeadService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<TenderLeadResponseDto>($"TenderLead #{id} not found.");
            return this.OkResponse(result);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<TenderLeadResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderLeadService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TenderLeadResponseDto>>> Create([FromBody] TenderLeadCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderLeadService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] TenderLeadUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.TenderLeadService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.TenderLeadService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }

        /// <summary>Converts a TenderLead into a formal Tender.</summary>
        [HttpPost("{id:int}/convert-to-tender")]
        public async Task<ActionResult<ApiResponse<TenderResponseDto>>> ConvertToTender(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderLeadService.ConvertToTenderAsync(id, cancellationToken);
            return this.OkResponse(result, "TenderLead converted to Tender successfully.");
        }
    }
}
