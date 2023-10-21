using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Consul.Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {

        private readonly IConsulClient _consulClient;

        public ServicesController(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }


        [HttpGet("GetServices")]
        public async Task Get()
        {
            try
            {

                /*

                v1/catalog/service/{ServiceName}

                all service:
                v1/catalog/services
            
                 */



                /*************************************************************/
                //var aa = new
                //{
                //    id = "1",
                //    name = "node_exporter",
                //    address = "192.157.4.8",
                //    port = 9100
                //};



                //var req = new HttpRequestMessage()
                //{
                //    Method = HttpMethod.Put,
                //    RequestUri = new Uri("http://localhost:60171/catalog/register")
                //};

                //req.Content = new StringContent(JsonConvert.SerializeObject(aa));

                //req.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                //req.Headers.TryAddWithoutValidation("X-Consul-Token", "91e121be-0667-edf3-4239-d322f6d0d5d9");


                //using (var client = new HttpClient())
                //{
                //    var result = client
                //        .SendAsync(req)
                //        .GetAwaiter().GetResult();

                //    Console.WriteLine(result);

                //    Console.WriteLine();
                //    //Console.WriteLine(result.IsSuccessStatusCode);

                //    //if (result.IsSuccessStatusCode)
                //    //{
                //    //    var model = result.Content.ReadAsStringAsync()
                //    //        .GetAwaiter().GetResult();

                //    //    Console.WriteLine(model);
                //    //}
                //    //else
                //    //{
                //    //    Console.WriteLine(result.RequestMessage);
                //    //}

                //}


                /*************************************************************/
                

                var q = new QueryOptions()
                {
                    Datacenter = "dc11"
                };


                var searchWithName = _consulClient
                    .Catalog.Service("rq", "", q)
                    .GetAwaiter().GetResult();

                
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}