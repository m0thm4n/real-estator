﻿using Google.Maps;
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
            GoogleMaps google = new GoogleMaps();

            GoogleSigned.AssignAllServices(new GoogleSigned(apiKey));

            var map = new StaticMapRequest();
            map.Center = new Location(address);
            map.Size = new System.Drawing.Size(600, 600);
            map.Zoom = 10;
            map.Markers.Add(address);

            return map;
        }

        public GoogleMaps() { }
    }
}
