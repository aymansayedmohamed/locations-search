using Roamler.LocationSearch.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roamler.LocationSearch.Infrastructure
{
    public interface IElasticService
    {
        Task<IEnumerable<LocationsSearchResult>> GetLocations(Location location, int maxDistance, int maxResults);
    }
}
