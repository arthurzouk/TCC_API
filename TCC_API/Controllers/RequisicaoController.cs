using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TCC_API.Context;
using TCC_API.Models;
using TCC_API.Repositories;

namespace TCC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequisicaoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<RequisicaoResult>>>  Get([FromServices] CrawlerContext context,
            [FromQuery]string nomeProduto, [FromQuery]string nomeMarca
            )
        {
            ProcedureRepository repositorio = new ProcedureRepository(context);

            try
            {
                var idRegistro = repositorio.EnviarRequisicao(nomeProduto, nomeMarca);


                List<RequisicaoResult> resultadoDaConsulta = new List<RequisicaoResult>();

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while (stopwatch.Elapsed.Seconds < 2)
                {
                    Thread.Sleep(100);
                }
                while (stopwatch.Elapsed.Seconds >= 2 && stopwatch.Elapsed.Seconds <= 60)
                {
                    resultadoDaConsulta = repositorio.ConsultarResultadoDaRequisicao(idRegistro);

                    if (resultadoDaConsulta != null)
                    {
                        if (resultadoDaConsulta.Count <= 9)
                        {
                            Thread.Sleep(1000);
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                stopwatch.Stop();

                if (resultadoDaConsulta == null) return new List<RequisicaoResult>();

                return resultadoDaConsulta;
            }
            catch (Exception ex)
            {
                return new List<RequisicaoResult>();
            }
        }
    }
}
