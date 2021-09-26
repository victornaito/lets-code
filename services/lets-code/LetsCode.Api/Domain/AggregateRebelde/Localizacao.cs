namespace LetsCode.Api.Domain.AggregateRebelde
{
    public class Localizacao
    {
        public Localizacao(string nome, string latitude, string longitude)
        {
            Nome = nome;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Nome { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}