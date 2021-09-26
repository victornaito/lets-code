using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using LetsCode.Api;
using LetsCode.Api.Application.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using static LetsCode.Api.Application.Commands.AdicionarRebeldeCommand;
using static LetsCode.Api.Application.Queries.RebeldeQueriesDTO.RelatorioDTO;

namespace IntegrationTests
{

    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public IntegrationTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            this._httpClient ??= webApplicationFactory
                .WithWebHostBuilder(_ => _.UseTestServer())
                .CreateClient();
        }

        private async Task<IReadOnlyCollection<RebeldeDTO>> ObterRebeldes()
        {
            return await _httpClient.GetFromJsonAsync<IReadOnlyCollection<RebeldeDTO>>("Rebelde/todos");
        }

        private async Task AdicionarRebelde()
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
            var jsonDeserialized = JsonConvert.DeserializeObject<AdicionarRebeldeCommand>(json);
            var jsonSerialized = JsonConvert.SerializeObject(jsonDeserialized);
            var data = new StringContent(jsonSerialized, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Rebelde/", data);

            Assert.Equal(HttpStatusCode.OK.GetHashCode(), response.StatusCode.GetHashCode());
        }

        [Fact]
        public async void Rebelde_AtualizarLocalizacao()
        {
            await AdicionarRebelde();
            var rebeldes = await ObterRebeldes();
            var atualizarCommand = new AtualizarLocalizacaoRebeldeCommand
            {
                Localizacao = new LocalizacaoDTO
                {
                    Nome = "Atualizado",
                    Latitude = "1234554",
                    Longitude = "123454546"
                },
                RebeldeId = rebeldes.First().Id
            };

            var data = new StringContent(JsonConvert.SerializeObject(atualizarCommand), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"Rebelde/{rebeldes.First().Id}/localizacao", data);

            var rebeldesAtualizado = await ObterRebeldes();
            Assert.Equal("Atualizado", rebeldesAtualizado.First().Localizacao.Nome);
            Assert.Equal("1234554", rebeldesAtualizado.First().Localizacao.Latitude);
            Assert.Equal("123454546", rebeldesAtualizado.First().Localizacao.Longitude);
        }

        [Fact]
        public async void ObterRelatorio_NaoDeveRetornarNulo()
        {
            var response = await _httpClient.GetAsync("Rebelde/");

            Assert.Equal(HttpStatusCode.OK.GetHashCode(), response.StatusCode.GetHashCode());
        }

        [Fact]
        public async void Rebelde_ReportarTraidor()
        {
            await AdicionarRebelde();
            var rebeldes = await ObterRebeldes();

            var reportarCommand = new ReportarRebeldeComoTraidorCommand
            {
                RebeldeId = rebeldes.First().Id
            };

            var data = new StringContent(JsonConvert.SerializeObject(reportarCommand), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"Rebelde/reportar-traidor", data);
            await _httpClient.PutAsync($"Rebelde/reportar-traidor", data);
            await _httpClient.PutAsync($"Rebelde/reportar-traidor", data);

            var rebeldesAtualizado = await ObterRebeldes();

            Assert.Equal(3, rebeldesAtualizado.First().Traicoes);
        }
    }
}
