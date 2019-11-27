using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_backend.IncidentRest.JsonObjects;
using mav_incident_dba;
using mav_incident_dba.Entities;
using mav_incident_rest.HttpServer;
using mav_incident_rest.HttpServer.Exceptions;
using mav_incident_rest.RestService.Endpoints;

namespace mav_incident_rest.IncidentRest.Endpoints
{
    public class LocationListing : RestService.Endpoints.RestEndpoint
    {

        public override string Path => "/location";

        public override HttpRequestMethod Method => HttpRequestMethod.GET;

        protected override RestResponse process(HttpRequest request, Dictionary<string, string> urlParams)
        {

            RestResponse resp = RestResponse.GetDefault();

            List<LocationListingEntry> locationEntries = new List<LocationListingEntry>();
            IncidentDatabase.Instance.Context.Locations
                .OrderBy(loc => loc.Name)
                .ToList()
                .ForEach(loc => locationEntries.Add(new LocationListingEntry(loc)));

            resp.Body = locationEntries;
            return resp;

        }

    }
}
