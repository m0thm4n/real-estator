using RealEstator.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zillow.Services;
using Zillow.Services.Schema;

namespace RealEstator.Zillow
{
    public class Zillow
    {
        public void GetHouse(string street, string zip)
        {
            var apiKey = GetConfig.LoadConfig();

            foreach(var item in apiKey)
            {
                ZillowClient client = new ZillowClient(item.zillow);
                Task<searchresults> search = client.GetSearchResultsAsync(street, zip);

                foreach (SimpleProperty prop in search.Result.response.results)
                {
                    var zest = client.GetZestimateAsync(prop.zpid.ToString());

                    var chart = client.GetChartAsync(prop.zpid.ToString(), "dollar", "600", "300");

                    var regionChart = client.GetRegionChartAsync("", "", "", prop.address.zipcode, "dollar", "600", "300", SimpleChartDuration.Item1year, ChartVariant.detailed);

                    var comp = client.GetCompsAsync(prop.zpid.ToString(), "10");

                    Task.WaitAll(zest, chart, regionChart, comp);



                }
            }
        }   
    }
}
