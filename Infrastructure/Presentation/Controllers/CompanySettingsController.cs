using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.CompanySettings;

namespace Presentation.Controllers
{
    /// <summary>
    /// Manages company settings (single-row table — GET + PUT only).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CompanySettingsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompanySettingsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CompanySettingsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<CompanySettingsResponseDto>>> Get(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.CompanySettingsService.GetAllAsync(cancellationToken);
            var settings = result.FirstOrDefault();
            if (settings is null)
                return this.NotFoundResponse<CompanySettingsResponseDto>("Company settings not configured.");
            return this.OkResponse(settings);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] CompanySettingsUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.CompanySettingsService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Company settings updated successfully.");
        }
    }
}