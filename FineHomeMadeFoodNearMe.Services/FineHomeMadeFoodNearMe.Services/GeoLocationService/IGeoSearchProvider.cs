namespace FineHomeMadeFoodNearMe.Services.GeoLocationService
{
    public interface IGeoSearchProvider
    {
        double[] FindGeoLocationByAddress(string state, string zipCode, string city, string address);
    }
}
