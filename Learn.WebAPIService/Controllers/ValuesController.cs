using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Learn.WebAPIService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values

        List<KeyValuePair<string, string>> dummyList = new List<KeyValuePair<string, string>>();
        
        public ValuesController()
        {
            dummyList.Add(new KeyValuePair<string, string>("first", "value1"));
            dummyList.Add(new KeyValuePair<string, string>("second", "value2"));

        }

        //[Route("Values/GetAll")]
        public List<KeyValuePair<string, string>> GetAll()
        {
            return dummyList;
        }

        [Authorize]
        [Route("Values/Get")]
        public string Get(string key)
        {
            return dummyList.Where(o => o.Key == key).FirstOrDefault().Value;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
