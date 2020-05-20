namespace FeatureManagementSample.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.FeatureManagement;
    using Microsoft.FeatureManagement.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;

        public FeatureController(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        [HttpGet]
        [FeatureGate(nameof(Features.FeatureA))]
        public async Task<IActionResult> Get()
        {          
            var featureA = await _featureManager.IsEnabledAsync(nameof(Features.FeatureA));
            var featureB = await _featureManager.IsEnabledAsync(nameof(Features.FeatureB));
            var featureC = await _featureManager.IsEnabledAsync(nameof(Features.FeatureC));
            var featureD = await _featureManager.IsEnabledAsync(nameof(Features.FeatureD));

            return Ok(featureD);
        }
    }
}