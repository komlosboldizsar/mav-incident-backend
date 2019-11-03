﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_backend.HttpServer
{
    public enum HttpResponseCode
    {
        S_400_BadRequest = 400,
        S_403_Forbidden = 403,
        S_404_NotFound = 404,
        S_500_InternalError = 500
    }
}
