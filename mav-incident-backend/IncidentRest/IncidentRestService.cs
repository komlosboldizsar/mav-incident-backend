﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_backend.RestService.Endpoints;
using mav_incident_backend.IncidentRest.Endpoints;

namespace mav_incident_backend.IncidentRest
{
    public class IncidentRestService : RestService.RestService
    {
        public IncidentRestService(int port) : base(port)
        { }

        protected override List<RestEndpoint> getEndpoints()
        {
            List<RestEndpoint> endpoints = new List<RestEndpoint>();
            endpoints.Add(new IncidentListing());
            endpoints.Add(new IncidentDetails());
            return endpoints;
        }
    }
}
