using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Sources")]
    [ApiController]
    public class SourcesCtr : ControllerBase
    {
        // GET: api/<Source>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Source>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Source>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Source>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Source>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
