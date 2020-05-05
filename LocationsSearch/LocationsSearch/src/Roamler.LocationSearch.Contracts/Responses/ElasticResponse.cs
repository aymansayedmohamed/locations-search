using Roamler.LocationSearch.Contracts.Models;

namespace Roamler.LocationSearch.Contracts.Responses
{
    public class ElasticResponse
    {
        public string Address { get; set; }
        public Geoip Geoip { get; set; }

    }
}
