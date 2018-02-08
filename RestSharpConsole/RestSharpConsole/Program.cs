using ClassLibrary1;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var testGuid = Guid.NewGuid();

            List<LogMessage> test = new List<LogMessage>() {
                new LogMessage("test",testGuid)
            };
            var client = new RestClient("http://localhost:1964");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("api/log/info/{identity}", Method.POST);
            request.AddParameter("type", "POST"); // adds to POST or URL querystring based on Method
            request.AddUrlSegment("identity", Guid.NewGuid().ToString()); // replaces matching token in request.Resource
            request.AddJsonBody(test);
            // easily add HTTP Headers
            request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            // execute the request
            var response = client.Post<Guid>(request);
            //IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            
            if(content == testGuid.ToString())
            {
                Console.WriteLine("SUCCESS");
                content = SimpleJson.DeserializeObject<string>(content);
            }
            else if(response.ErrorMessage != null)
            {
                //foreach (var item in response.ErrorMessage.GetType().GetProperties())
                //{
                //    Console.WriteLine(item.GetValue(item));
                //}
                Console.WriteLine(response.ErrorMessage);
            }
            else if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.StatusDescription);
            }
            Console.ReadLine();

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //RestResponse<Person> response2 = client.Execute<Person>(request);
            //var name = response2.Data.Name;

            // easy async support
            //client.ExecuteAsync(request, response => {
            //    Console.WriteLine(response.Content);
            //});

            // async with deserialization
            //var asyncHandle = client.ExecuteAsync<Person>(request, response => {
            //    Console.WriteLine(response.Data.Name);
            //});

            // abort the request on demand
            //asyncHandle.Abort();
        }
    }
}
