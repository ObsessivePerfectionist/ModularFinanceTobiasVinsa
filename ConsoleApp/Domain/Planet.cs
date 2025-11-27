namespace ConsoleApp.Domain
{
    public class Planet
    {
        public int DisplayId { get; set; }
        public required string SwapiIdUrl { get; set; }
        public required string Name { get; set; }
        public int NumberOfResidents { get; set; }
    }
}
