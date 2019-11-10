using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer
{
    public class HttpServer
    {

        private int port;

        public delegate void RequestHandlerDelegate(HttpRequest request, HttpResponse response);
        public event RequestHandlerDelegate RequestHandler;

        public HttpServer(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            bool done = false;

            var listener = new TcpListener(IPAddress.Any, port);

            listener.Start();

            while (!done)
            {

                Console.Write("Waiting for connection... ");
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Connection accepted.");
                NetworkStream ns = client.GetStream();

                List<byte> bytes = new List<byte>();
                string requestString = "";
                while (!requestString.Contains("\r\n\r\n"))
                {
                    while (!ns.DataAvailable) ;
                    byte[] buffer = new byte[1024];
                    ns.Read(buffer, 0, 1024);
                    bytes.AddRange(buffer);
                    requestString = Encoding.UTF8.GetString(bytes.ToArray());
                }
                HttpRequest request = HttpRequest.CreateFromString(requestString);

                HttpResponse response = new HttpResponse();

                try
                {
                    RequestHandler?.Invoke(request, response);
                }
                catch (Exception e)
                {
                    response = new HttpExceptionResponse(e);
                    Console.WriteLine(e);
                }

                try
                {
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response.GetResponseString());
                    ns.Write(responseBytes, 0, responseBytes.Length);
                    ns.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }

            listener.Stop();

        }

        public void Stop()
        {

        }

    }
}
