using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestSharpClientApi.Controllers
{
    [RoutePrefix("api/Log")]
    public class LogController : ApiController
    {
        // GET: api/Log
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Log/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Log
        public void Post([FromBody]string value)
        {
        }
        [HttpPost]
        [Route("Info/{identity}")]
        public Guid LogInfo(Guid identity, List<LogMessage> message)
        {
            if (message.Count() == 0 || message == null)
            {
                //throw new ArgumentNullException(nameof(message));
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            return identity;
        }

        // PUT: api/Log/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Log/5
        public void Delete(int id)
        {
        }
    }
}
