using Google.Maps;
using Google.Maps.Geocoding;
using Google.Maps.StaticMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAPI
{
    public class GoogleMaps
    {
        public StaticMapRequest GetMaps(string apiKey, string address)
        {
            GoogleSigned.AssignAllServices(new GoogleSigned(apiKey));

            var map = new StaticMapRequest();
            map.Center = new Location(address);
            map.Size = new System.Drawing.Size(400, 400);
            map.Zoom = 14;

            return map;
        }
    }
}
