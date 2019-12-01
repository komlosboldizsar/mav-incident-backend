using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer.RequestRouter
{

    public delegate void RouteHandlerDelegate(HttpRequest request, HttpResponse response, Dictionary<string, string> parameters);

    public class Route
    {

        public string Path { get; private set; }

        private Regex pathRegex;

        public HttpRequestMethod Method { get; private set; }

        private RouteHandlerDelegate handler;

        public Route(string path, HttpRequestMethod method, RouteHandlerDelegate handler)
        {
            this.Path = path;
            this.Method = method;
            this.handler = handler;
            createPathRegex();
        }

        private void createPathRegex()
        {

            string pattern = Path;
            if (pattern.Length < 1)
                return;

            if (pattern[0] == '/')
                pattern = pattern.Substring(1);

            pattern = Regex.Escape(pattern);
            pattern = pattern.Replace(@"\{", "{");
            pattern = pattern.Replace(@"\}", "}");

            string REPLACE_PATTERN = @"{([a-zA-Z0-9]+):([is])\}";
            pattern = Regex.Replace(pattern, REPLACE_PATTERN, PathRegexpReplacer);

            pattern = "^/?" + pattern + "$";

            pathRegex = new Regex(pattern);

        }

        public static string PathRegexpReplacer(Match match)
        {
            string paramName = match.Groups[1].Value;
            string paramType = match.Groups[2].Value;
            if (paramType == "i")
                return string.Format("(?<{0}>[0-9]+)", paramName);
            else
                return string.Format("(?<{0}>[^/]+)", paramName);
        }

        public bool Matches(HttpRequest request)
        {
            if (request.Method != Method)
                return false;
            if (!pathRegex.IsMatch(request.Path))
                return false;
            return true;
        }

        public void Process(HttpRequest request, HttpResponse response)
        {
            if (handler != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                GroupCollection groups = pathRegex.Match(request.Path).Groups;
                foreach (Group group in groups)
                    if (group.Name != "0")
                        parameters.Add(group.Name, group.Value);
                handler.Invoke(request, response, parameters);
            }
        }

    }
}