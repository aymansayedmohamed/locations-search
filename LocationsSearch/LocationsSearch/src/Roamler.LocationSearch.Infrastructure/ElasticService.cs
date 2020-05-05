using Microsoft.Extensions.Configuration;
using Nest;
using Roamler.LocationSearch.Contracts.Models;
using Roamler.LocationSearch.Contracts.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roamler.LocationSearch.Infrastructure
{
    public class ElasticService : IElasticService
    {
        private readonly IConfiguration _configuration;
        private readonly IElasticClient _elasticClient;
        public ElasticService(IConfiguration configuration, IElasticClient elasticClient)
        {
            _configuration = configuration;
            _elasticClient = elasticClient;
        }
        public async Task<IEnumerable<LocationsSearchResult>> GetLocations(Location location, int maxDistance, int maxResults)

        {
            var elasticDefaultIndex = _configuration.GetValue<string>("ElasticDefaultIndex");

            var query = new GeoDistanceQuery
            {
                Field = Infer.Field<ElasticResponse>(p => p.Geoip.location),
                DistanceType = GeoDistanceType.Arc,
                Location = new GeoLocation(location.lat, location.lon),
                Distance = $"{maxDistance}m"
            };

            var response = await _elasticClient.SearchAsync<ElasticResponse>(
                s => s.Index(elasticDefaultIndex).Query(q => query)
                    .Size(maxResults));

            return response.Documents.Select(o => new LocationsSearchResult()
            {
                Address = o.Address,
                Latitude = o.Geoip.location.lat,
                Longitude = o.Geoip.location.lon
            });
        }
    }
}
