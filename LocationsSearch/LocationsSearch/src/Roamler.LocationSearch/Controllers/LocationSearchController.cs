using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Roamler.LocationSearch.Contracts.Models;
using Roamler.LocationSearch.Infrastructure;
using Roamler.LocationSearch.Models;

namespace Roamler.LocationSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationSearchController : ControllerBase
    {
        private readonly ILogger<LocationSearchController> _logger;
        private readonly IElasticService _elasticService;

        public LocationSearchController(ILogger<LocationSearchController> logger, IElasticService elasticService)
        {
            _logger = logger;
            _elasticService = elasticService;
        }

        [HttpPost]
        [Route("GetLocations")]
        public async Task<IEnumerable<LocationsSearchResult>> GetLocationsAsync(LocationSearchRequest request)
        {
            return await _elasticService.GetLocations(new Location() { lat = request.Lat, lon = request.Lon },request.MaxDistance, request.MaxResults);
        }
    }
}