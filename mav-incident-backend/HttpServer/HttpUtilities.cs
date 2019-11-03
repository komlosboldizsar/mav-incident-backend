using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_backend.HttpServer
{
    public static class HttpUtilities
    {

        public static Dictionary<HttpResponseCode, string> reasonPhrases = new Dictionary<HttpResponseCode, string>() {
            { HttpResponseCode.S_200_Success, "Success" },
            { HttpResponseCode.S_400_BadRequest, "Bad Request" },
            { HttpResponseCode.S_403_Forbidden, "Forbidden" },
            { HttpResponseCode.S_404_NotFound, "Not Found" },
            { HttpResponseCode.S_500_InternalError, "Internal Server Error" },
        };

        public static string ToReasonPhrase(this HttpResponseCode code)
        {
            if (reasonPhrases.TryGetValue(code, out string message))
                return message;
            return "";
        }

        public static HttpRequestMethod? RequestMethodFromString(string requestMethodStr)
        {
            foreach (HttpRequestMethod requestMethod in Enum.GetValues(typeof(HttpRequestMethod)))
            {
                if (requestMethod.ToString() == requestMethodStr)
                    return requestMethod;
            }
            return null;
        }

    }
}
