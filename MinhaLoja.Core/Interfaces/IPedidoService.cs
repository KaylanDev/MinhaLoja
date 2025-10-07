using FluentResults;
using MinhaLoja.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Interfaces
{
    public interface IPedidoService
    {
        public Task<Result<Pedido>> CriarPedidoAsync(ObjectId usuarioId);
        public Task<Result> CancelarPedido(ObjectId usuarioId,ObjectId pedidoId);
       public Task<Result> MarcarComoPagoParaEnvioAsync(ObjectId pedidoId, string transacaoId);
        public Task<Result> MarcarComoEnviadoAsync(ObjectId pedidoId, string codigoRastreio);
       public Task<Result>  MarcarComoEntregueAsync(string pedidoId);
        //Consulta e Histórico
        public Task<Result<Pedido>> ObterPorIdAsync(string pedidoId);
        public Task<Result<IEnumerable<Pedido>>> ObterPorUsuarioIdAsync(string usuarioId);
        public Task <Result<IEnumerable<Pedido>>> ObterTodosParaAdminAsync(string status);


    }
}
