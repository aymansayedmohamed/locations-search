
namespace Roamler.LocationSearch.Models
{
    public class LocationSearchRequest
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int MaxDistance { get; set; }
        public int MaxResults { get; set; }
    }
}
