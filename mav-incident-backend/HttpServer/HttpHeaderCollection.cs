using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_backend.HttpServer
{
    public class HttpHeaderCollection : IEnumerable<KeyValuePair<string, string>>
    {

        protected Dictionary<string, string> headers = new Dictionary<string, string>();

        public string this[string key]
        {
            get { return getHeader(key); }
            set { setHeader(key, value); }
        }

        public int Count => headers.Count;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return headers.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return headers.GetEnumerator();
        }

        protected virtual string getHeader(string key)
        {
            if (headers.TryGetValue(key, out string value))
                return value;
            return null;
        }

        protected virtual void setHeader(string key, string value)
        {
            headers[key] = value;
        }

    }
}
