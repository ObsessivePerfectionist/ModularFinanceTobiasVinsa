namespace ConsoleApp.Domain
{
    public class SwapiPlanetResponse
    {
        public int Count { get; set; }
        public List<SwapiPlanetDto> Results { get; set; } = [];
    }
}
