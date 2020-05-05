using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TCC_API.Context;
using TCC_API.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace TCC_API.Repositories
{
    public class ProcedureRepository
    {
        private CrawlerContext _context;

        public ProcedureRepository(CrawlerContext context)
        {
            _context = context;
        }

        internal int EnviarRequisicao(string nomeProduto, string nomeMarca)
        {
            var idCriado = _context.Database.GetDbConnection()
                .Query<int>(
                    "API_ENVIAR_REQUISICAO",
                    new { nomeProduto, nomeMarca },
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure)
                .FirstOrDefault();
            return idCriado;
        }

        internal List<RequisicaoResult> ConsultarResultadoDaRequisicao(int idRequisicao)
        {
            var retornoProc = _context.Database.GetDbConnection()
                .Query<RequisicaoResult>(
                    "API_RETORNAR_RESULTADO_REQUISICAO",
                    new { idRequisicao },
                    commandTimeout: null,
                    commandType: CommandType.StoredProcedure)
                .ToList();
            return retornoProc;
        }

        ///Referencia
        //public ConexaoBuscarRetornoViabilidade_Result ConexaoBuscarRetornoViabilidade(long idVenda)
        //{
        //    var retornoProc = _context.Database.Connection
        //        .Query<ConexaoBuscarRetornoViabilidade_Result>(
        //            "INT_PAM_CONEXAO_BUSCAR_RETORNO_VIABILIDADE",
        //            new { idVenda },
        //            commandTimeout: null,
        //            commandType: CommandType.StoredProcedure)
        //        .FirstOrDefault();
        //    return retornoProc;
        //}

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
