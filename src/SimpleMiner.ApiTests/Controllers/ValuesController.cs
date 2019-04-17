﻿using Microsoft.AspNetCore.Mvc;
using SimpleMiner.Navigation.Http;
using SimpleMiner.Parsing.Html;
using SimpleMiner.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleMiner;
using System.IO;

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

            var pdfResponse = await navigator.DownloadFile(
                "http://sd.blackball.lv/library/NET_Domain-Driven_Design_with_CSharp_-_Problem-Design-Solution.pdf");

            return Ok(pdfResponse);

           System.IO.File.WriteAllText("conteudo.pdf", pdfResponse.Content);

            var httpResponse = await navigator
                .GetAsync("https://www3.tjrj.jus.br/segweb/faces/login.jsp?indGet=true&SIGLASISTEMA=PORTALSERV");

            var parser = _minerService
                .UseParser<HtmlParser>(httpResponse.Content);

            var nodeCollection = parser
                .FromNodeCollection(ParseBy.Xpath("//table/tbody"))
                .ForEachSingleNode(ParseBy.Xpath("./tr"))
                .ParseInnerTextCollection();

            var form = parser.GetForm(ParseBy.Id("formLogin"));
            form.Values["txtLogin"] = "37661264749";
            form.Values["txtSenha"] = "12345678";
            form.Values["source"] = "btnEnviar";

            httpResponse = await navigator
                .SubmitForm(form);

            httpResponse = await navigator
                .GetAsync("http://www4.tjrj.jus.br/portalDeServicos/processoeletronico");

            httpResponse = await navigator
                .GetAsync("http://www4.tjrj.jus.br/portalDeServicos/jsp/portal/redirPortalNovaJanela.jsp");

            var parameters = new Dictionary<string, string>
            {
                { "node", "xnode-21" }
            };

            httpResponse = await navigator
                .PostAsync<string>("http://www4.tjrj.jus.br/portalDeServicos/treejson", parameters);

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
