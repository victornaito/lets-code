using System;
using System.Collections.Generic;
using System.Linq;
using LetsCode.Api.Domain.AggregateRebelde;
using Shared.Domain;

namespace LetsCode.Api.Domain.AggregateRebelde
{
    public class Rebelde : EntityGuid
    {
        private readonly ushort QUANTIDADE_MAXIMA_TRAICOES = 3;

        public Rebelde(Guid id, string nome, uint idade, string genero, ushort traicoes, Localizacao localizacao, Inventario inventario)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            Genero = genero;
            Traicoes = traicoes;
            Localizacao = localizacao;
            Inventario = inventario;
        }

        public Rebelde(string nome, uint idade, string genero, ushort traicoes, Localizacao localizacao, Inventario inventario)
        {
            Nome = nome;
            Idade = idade;
            Genero = genero;
            Traicoes = traicoes;
            Localizacao = localizacao;
            Inventario = inventario;
        }

        public Rebelde()
        {
        }

        public string Nome { get; private set; }
        public uint Idade { get; private set; }
        public string Genero { get; private set; }
        public ushort Traicoes { get; private set; }
        public Localizacao Localizacao { get; private set; }
        public Inventario Inventario { get; private set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public void AlterarLocalizacao(Localizacao localizacao)
        {
            Localizacao = localizacao;
        }

        public void AlterarRecursos(ICollection<Recurso> recursos)
        {
            var recursoCadastrado = Inventario.Recursos.ToList();
            foreach (var recurso in recursos)
            {
                recursoCadastrado.Add(recurso);
            }
            Inventario.AlterarRecursos(recursoCadastrado);
        }

        public void RemoverRecursos(ICollection<Guid> recursosID)
        {
            Inventario.AlterarRecursos(Inventario.Recursos.Where(_ => !recursosID.Contains(_.Id)).ToArray());
        }

        public bool Traidor() => Traicoes >= QUANTIDADE_MAXIMA_TRAICOES;

        public void AdicionarTraicao()
        {
            Traicoes++;
        }
    }
}