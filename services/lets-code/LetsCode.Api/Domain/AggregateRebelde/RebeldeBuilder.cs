using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static LetsCode.Api.Application.Commands.AdicionarRebeldeCommand;

namespace LetsCode.Api.Domain.AggregateRebelde
{
    public sealed class RebeldeBuilder
    {
        private Guid id;
        private string genero;
        private uint idade;
        private string nome;
        private ushort traicoes;
        private Inventario inventario;
        private Localizacao localizacao;

        public RebeldeBuilder ComId(Guid id)
        {
            this.id = id;
            return this;
        }

        public RebeldeBuilder ComGenero(string genero)
        {
           this.genero = genero;
           return this;
        }

        public RebeldeBuilder ComIdade(uint idade)
        {
            this.idade = idade;
            return this;
        }

        public RebeldeBuilder ComNome(string nome)
        {
            this.nome = nome;
            return this;
        }

        public RebeldeBuilder ComTraidor(ushort traicao)
        {
            this.traicoes = traicao;
            return this;
        }

        public RebeldeBuilder ComLocalizacao(LocalizacaoDTO dto)
        {
            localizacao = new Localizacao(dto.Nome, dto.Latitude, dto.Longitude);
            return this;
        }

        public RebeldeBuilder ComInventario(InventarioDTO dto)
        {
            var recursos = new Collection<Recurso>();

            foreach (var recurso in dto.Recursos)
                recursos.Add(new Recurso(recurso.Tipo, recurso.Quantidade));

            inventario = new Inventario(recursos);
            return this;
        }

        public Rebelde Build() =>
            new Rebelde(nome, idade, genero, traicoes, localizacao, inventario);
    }
}
