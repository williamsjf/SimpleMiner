using Microsoft.AspNetCore.Mvc;
using SimpleMiner.Navigation.Http;
using SimpleMiner.Parsing.Html;
using SimpleMiner.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var navigator = _minerService
                .UseNavigator<IHttpNavigator>();

            var httpResponse = await navigator
                .GetAsync("https://www3.tjrj.jus.br/segweb/faces/login.jsp?indGet=true&SIGLASISTEMA=PORTALSERV");

            var parser = _minerService
                .UseParser<IHtmlParser>(httpResponse.Content);

            var forms = parser.GetForms(ParseBy.Id("formLogin"));

            var parameters = new Dictionary<string, string>
            {
                { "_id5", "https://www3.tjrj.jus.br/autenticacaoPKI/autenticacaopki" },
                { "_noJavaScript", "false" },
                { "org.apache.myfaces.trinidad.faces.FORM", "formLogin" },
                { "org.apache.myfaces.trinidad.faces.STATE", "!-9fowmj7le" },
                { "source", "btnEnviar" },
                { "txtLogin", "37661264749" },
                { "txtSenha", "12345678" },
            };

            httpResponse = await navigator
                .PostAsync<string>("https://www3.tjrj.jus.br/segweb/faces/login.jsp", parameters);

            httpResponse = await navigator
                .GetAsync("http://www4.tjrj.jus.br/portalDeServicos/processoeletronico");

            httpResponse = await navigator.GetAsync("http://www4.tjrj.jus.br/portalDeServicos/jsp/portal/redirPortalNovaJanela.jsp");

            var dic = new Dictionary<string, string>
            {
                { "node", "xnode-21" }
            };

            httpResponse = await navigator.PostAsync<string>("http://www4.tjrj.jus.br/portalDeServicos/treejson", dic);

            return Ok(httpResponse.Content);
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
