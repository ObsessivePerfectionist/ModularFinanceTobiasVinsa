namespace ConsoleApp.Domain
{
    public class SwapiPlanetDto
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        public List<string> Residents { get; set; } = [];
    }
}
