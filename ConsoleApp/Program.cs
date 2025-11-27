using ConsoleApp.Helpers;
using ConsoleApp.Services;

Console.WriteLine("Welcome to the star wars universe data terminal! \n" +
    "Below you can see a list of star wars planets that exists \n");

var service = new SwapiService();
var planets = await service.GetPlanetsQuery();

foreach (var planet in planets)
{
    Console.WriteLine($"{planet.DisplayId + 1}. {planet.Name} ({planet.NumberOfResidents} residents)\n");
}


Console.WriteLine("Please type in an Id of the planet you want to know more about the residents of the planet");

while (true) {
    var userInput = Console.ReadLine();
    if (int.TryParse(userInput, out int value))
    {
        if (value > 0 && value <= planets.Count)
        {
            int index = value - 1;
            var swapiPlanetIdUrl = planets[index].SwapiIdUrl;
            var swapiPlanetId = UrlHelper.SwapiIdHelperFunction(swapiPlanetIdUrl);
            var residents = await service.GetPeopleOnPlanetQuery(swapiPlanetId);
            if (string.IsNullOrWhiteSpace(residents))
                Console.WriteLine("No one lives here");
            else
                Console.WriteLine(residents);
        }
        else Console.WriteLine("Please select a number from the displayed list of planets");
    }
    else Console.WriteLine("Invalid Id(number)! Please try again.\n");
}