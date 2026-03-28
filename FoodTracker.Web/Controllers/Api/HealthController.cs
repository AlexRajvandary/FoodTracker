using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FoodTracker.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;
    public HealthController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var report = await _healthCheckService.CheckHealthAsync(cancellationToken).ConfigureAwait(false);
        return report.Status == HealthStatus.Healthy
            ? Ok(new { status = report.Status.ToString() })
            : StatusCode(StatusCodes.Status503ServiceUnavailable, new { status = report.Status.ToString() });
    }
}
