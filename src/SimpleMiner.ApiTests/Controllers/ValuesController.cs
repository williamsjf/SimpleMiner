using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMiner.Service;

namespace SimpleMiner.ApiTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMinerService _minerService;

        public ValuesController(IMinerService minerService)
        {
            _minerService = minerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _minerService.UseHttpNavigator()
                .GetAsync("http://www.tjrj.jus.br/");

            return Ok(result.Content);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
