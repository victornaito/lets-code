using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shared.Domain;

namespace LetsCode.Api.Domain.AggregateRebelde
{
    public class Inventario : EntityGuid
    {
        public Inventario(ICollection<Recurso> recursos)
        {
            Recursos = recursos;
        }

        public ICollection<Recurso> Recursos { get; private set; } = new Collection<Recurso>();

        public void AlterarRecursos(ICollection<Recurso> recursos)
        {
            Recursos = recursos;
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}