using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Models.Enums
{
    public enum Status
    {
        PagamentoPendente,
        Processando,
        AguardandoEnvio,
        Enviado,
        Entregue,
        Cancelado
    }
}
