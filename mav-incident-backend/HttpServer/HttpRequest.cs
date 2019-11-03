using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_backend.HttpServer
{
    public class HttpRequest
    {

        public HttpRequestMethod Method { get; private set; }

        public string Path { get; private set; }

        public string Version { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        private HttpRequest(HttpRequestMethod method, string path, string version, Dictionary<string, string> headers, string body)
        {
            this.Method = method;
            this.Path = path;
            this.Version = version;
            this.Headers = new HttpHeaderReadOnlyCollection(headers);
            this.Body = body;
        }

        public static HttpRequest CreateFromString(string requestString)
        {

            string[] parts = requestString.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
            if (parts.Length < 1)
                throw new Exceptions.BadRequestException();

            string[] lines = parts[0].Split(new string[] { "\r\n" }, StringSplitOptions.None);
            if (lines.Length < 1)
                throw new Exceptions.BadRequestException();

            string[] firstLine = lines[0].Split(' ');
            if (firstLine.Length != 3)
                throw new Exceptions.BadRequestException();
            HttpRequestMethod? requestMethod = HttpUtilities.RequestMethodFromString(firstLine[0]);
            if(requestMethod == null)
                throw new Exceptions.BadRequestException();
            string path = firstLine[1];
            string version = firstLine[2];

            Dictionary<string, string> headers = new Dictionary<string, string>();
            for(int i = 1; i < lines.Length; i++)
            {
                string[] headerParts = lines[i].Split(new string[] { ": " }, StringSplitOptions.None);
                if(headerParts.Length < 2)
                    throw new Exceptions.BadRequestException();
                headers.Add(headerParts[0], headerParts[1]);
            }

            string body = (parts.Length >= 2) ? parts[1] : "";

            HttpRequest req = new HttpRequest((HttpRequestMethod)requestMethod, path, version, headers, body);
            return req;

        }

    }
}
