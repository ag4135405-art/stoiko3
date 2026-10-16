using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace stoiko3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiServicioAPiController : ControllerBase
    {
        // GET: api/<MiServicioAPiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MiServicioAPiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MiServicioAPiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MiServicioAPiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MiServicioAPiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
