using System.Linq;

namespace FineHomeMadeFoodNearMe.Services.GeoLocationService
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Runtime.Serialization.Json;

    public sealed class BingGeoSearchProvider : IGeoSearchProvider
    {
        private const string BingMapKey = "AlXmunxcL98vZy4vRQoDgJLHwIuPGDMuDZhqFw1QyJHw18ko0dtBwaVhBpNfi-CN";

        private const string BingAddressSearchUrlTemplate =
            "http://dev.virtualearth.net/REST/v1/Locations/US/{0}/{1}/{2}/{3}?includeNeighborhood=0&maxResults=10&key={4}";

        public double[] FindGeoLocationByAddress(string state, string zipCode, string city, string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException("address");
            }
            if (string.IsNullOrWhiteSpace(state))
            {
                throw new ArgumentNullException("state");
            }
            if (string.IsNullOrWhiteSpace(state))
            {
                throw new ArgumentNullException("city");
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException("zipCode");
            }
            var uri =
                new Uri(
                    string.Format(CultureInfo.InvariantCulture, 
                                BingAddressSearchUrlTemplate, state, city, zipCode, Uri.EscapeDataString(address), BingMapKey));
            var response = GetResponse(uri);
            if (response == null || response.ResourceSets == null || response.ResourceSets.Length == 0)
            {
                return null;
            }
            var resourceSet = response.ResourceSets.FirstOrDefault();
            if (resourceSet == null || resourceSet.Resources == null || resourceSet.Resources.Length == 0)
            {
                return null;
            }
            var resource = resourceSet.Resources.First();
            return resource.Point == null ? null : resource.Point.Coordinates;
        }

        private BingMapSearchResponse GetResponse(Uri uri)
        {
            try
            {
                var request = WebRequest.Create(uri) as HttpWebRequest;
                if (request == null)
                {
                    return null;
                }
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response == null)
                    {
                        throw new InvalidOperationException("Response is not of type HttpWebResponse");
                    }
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                    }
                    var stream = response.GetResponseStream();
                    if (stream == null)
                    {
                        throw new InvalidOperationException("No response stream found");
                    }
                    var jsonSerializer = new DataContractJsonSerializer(typeof(BingMapSearchResponse));
                    return jsonSerializer.ReadObject(stream) as BingMapSearchResponse;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}