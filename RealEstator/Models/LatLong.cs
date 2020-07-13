using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Web.Razor.Text;
using BingMapsRESTToolkit;

namespace RealEstator.Models
{
    public class LatLong
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LatLong(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public LatLong()
        {

        }
    }


    public static class GeocodeHelper
    {
        public static LatLong Geocode(string address)
        {
            string url = "http://dev.virtualearth.net/REST/v1/Locations?query=" + address + "&key=AqDAHWGqF_8t8GLQd8-bYTXxSpB8pe88jnVHkrXbXSzDytBI9g2g-TJQG6jm7S5x";
            
            using (var client = new WebClient())
            {
                string response = client.DownloadString(url);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Response));
                using (var es = new MemoryStream(Encoding.Unicode.GetBytes(response)))
                {
                    var mapResponse = (serializer.ReadObject(es) as Response); //Response is one of the Bing Maps DataContracts
                    Location location = (Location)mapResponse.ResourceSets.First().Resources.First();
                    return new LatLong()
                    {
                        Latitude = location.Point.Coordinates[0],
                        Longitude = location.Point.Coordinates[1]
                    };
                }
            }
        }
    }
}