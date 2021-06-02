using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CA.Services.AuthoringService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoringRestController : ControllerBase
    {
        // GET: api/<AuthoringRestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthoringRestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthoringRestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthoringRestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthoringRestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
