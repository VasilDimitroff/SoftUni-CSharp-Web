using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    class StartUp
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            const string NewLine = "\r\n";
            TcpListener tcpListener = new TcpListener(
                IPAddress.Loopback, 80);
            tcpListener.Start();
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {
                    byte[] buffer = new byte[1000000];
                    var lenght = stream.Read(buffer, 0, buffer.Length);

                    string requestString =
                        Encoding.UTF8.GetString(buffer, 0, lenght);
                    Console.WriteLine(requestString);

                    string html = $"<h1>Hello from my custom server {DateTime.Now}</h1>" +
                        $"<form action=/filled method=post><input name=username /><input name=password />" +
                        $"<input type=submit /></form>";

                    string response = "HTTP/1.1 301 OK" + NewLine +
                        "Server: VasilServer 2021" + NewLine +
                        "Location: https://www.google.com" + NewLine +
                        "Content-Type: text/html; charset=utf-8" + NewLine +
                        // "Content-Disposition: attachment; filename=vasil.txt" + NewLine +
                        "Content-Lenght: " + html.Length + NewLine +
                        NewLine +
                        html + NewLine;

                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes);

                    Console.WriteLine(new string('=', 70));
                }
            }
        }

        public static async Task ReadData()
        {
            string url = "http://www.vdimitrov.net/";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(string.Join(Environment.NewLine,
                response.Headers.Select(x => x.Key + ": " + x.Value.First())));

            // var html = await httpClient.GetStringAsync(url);
            // Console.WriteLine(html);
        }
    }
}