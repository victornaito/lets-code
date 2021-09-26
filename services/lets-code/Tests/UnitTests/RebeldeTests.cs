using System;
using System.Linq;
using LetsCode.Api.Application.Commands;
using Newtonsoft.Json;
using Xunit;

namespace UnitTests
{
    public class RebeldeTests
    {
        [Fact]
        public void CommandCriarRebelde_RebeldeValido()
        {
            var json = @"
                    {
                      nome: 'rebelde 2',
                      idade: 0,
                      genero: 'masc',
                      localizacao: {
                                    nome: 'loc',
                        latitude: '232234',
                        longitude: 564765
                      },
                      inventario: {
                        recursos: [{
                                tipo: 1,
                                quantidade: 2
                            },
                            {
                                tipo: 2,
                                quantidade: 4
                            }
                        ]
                      }
                    }
                    ";
            var command = JsonConvert.DeserializeObject<AdicionarRebeldeCommand>(json);
            Assert.True(command.IsValid());
        }

        [Fact]
        public void CommandCriarRebelde_RebeldeInvalido()
        {
            var json = @"
                    {
                      nome: 'rebelde 2',
                      idade: 0,
                      genero: 'masc',
                      localizacao: {
                                    nome: 'loc',
                        latitude: '232234',
                        longitude: 564765
                      },
                      inventario: {
                        recursos: []
                      }
                    }
                    ";
            var command = JsonConvert.DeserializeObject<AdicionarRebeldeCommand>(json);
            Assert.False(command.IsValid());
            Assert.Equal("Informe ao menos um item na lista de Recursos", command.Validation().Errors.First().ErrorMessage);
        }
    }
}
