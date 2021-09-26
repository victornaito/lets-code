using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LetsCode.Api.Application.Commands.AdicionarRebeldeCommand;

namespace LetsCode.Api.Application.Queries
{
    public sealed class RebeldeQueriesDTO
    {
        public class RelatorioDTO
        {
            public double PorcentagemTraidores { get; set; }
            public double PorcentagemRebeldes { get; set; }
            public ICollection<RecursoPorRebeldeDTO> RecursosPorRebelde { get; set; }
            public uint PontosPerdidosPorContaDeTraidores { get; set; }

            public class RecursoPorRebeldeDTO
            {
                public string DescricaoRecurso { get; set; }
                public long QuantidadeRecurso { get; set; }
            }

            public class RebeldeDTO
            {
                public Guid Id { get; set; }
                public string Nome { get; set; }
                public uint Idade { get; set; }
                public string Genero { get; set; }
                public ushort Traicoes { get; set; }
                public LocalizacaoDTO Localizacao { get; set; }
                public InventarioDTO Inventario { get; set; }

            }
        }
    }
}