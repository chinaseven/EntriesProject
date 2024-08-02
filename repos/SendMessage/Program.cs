using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SendMessage
{
    internal class Program
    {
        static  void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            var request = new HttpRequestMessage();
            Uri uri = new Uri("http://localhost:5108/Financial/postEntry");
            request.RequestUri = uri;
            
            string payload = "Bruno";
            var requestBody = new StringContent("{\r\n    \"id\":\"1\",\r\n    \"type\": \"Credit\",\r\n    \"value\": \"200.00\",\r\n    \"date\": \"2024-01-01\"\r\n  \r\n}", Encoding.UTF8, new MediaTypeHeaderValue("application/vnd.api+json"));
            StringContent httpContent = new StringContent(payload,Encoding.UTF8, "application/vnd.api+json");
            // httpContent.Headers.ContentType("application/json");
            //httpContent.Headers.Add("Content-Type", "application/json");    
            for (int i = 0; i <20; i++) {
                var t = httpClient.PostAsync(uri.ToString(), requestBody);
                var msg = t.Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(msg); 
            }
            Console.ReadLine();
        }
    }
}
