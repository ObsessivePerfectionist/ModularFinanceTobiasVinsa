using System.Text.Json;
using ConsoleApp.Domain;

namespace ConsoleApp.Services;

public class SwapiService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    public static readonly string ALL_PLANETS_ENDPOINT = "https://swapi.dev/api/planets";

    public async Task<List<Planet>> GetPlanetsQuery()
    {
        var resp = await _httpClient.GetAsync(new Uri(ALL_PLANETS_ENDPOINT));
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Api call to swapi was not successful! StatusCode: " + resp.StatusCode);
            return [];
        }
        var json = await resp.Content.ReadAsStringAsync();
        var swapi = JsonSerializer.Deserialize<SwapiPlanetResponse>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (swapi == null)
            return [];

        //Response is returned in a resp list and easier to map it over to my class for presentation.
        return swapi.Results
            .OrderBy(p => p.Name).Select((p, index) => new Planet
            {
                DisplayId = index,
                SwapiIdUrl = p.Url,
                Name = p.Name,
                NumberOfResidents = p.Residents.Count
            })
            .ToList();
    }

    public async Task<string?> GetPeopleOnPlanetQuery(int planetId)
    {
        var resp = await _httpClient.GetAsync($"{ALL_PLANETS_ENDPOINT}/{planetId}/");
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Api call to swapi was not successful! StatusCode: " + resp.StatusCode);
            return null;
        }
        var json = await resp.Content.ReadAsStringAsync();
        var swapi = JsonSerializer.Deserialize<SwapiPlanetDto>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (swapi == null)
            return null;

        var residentTasks = swapi.Residents.Select(async url =>
        {
            try
            {
                var resp = await _httpClient.GetStringAsync(url);
                var resident = JsonSerializer.Deserialize<Resident>(resp, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return resident?.Name;
            }
            catch
            {
                return null;
            }
        });
        var residentNames = await Task.WhenAll(residentTasks);
        return string.Join(Environment.NewLine, residentNames);
    }
}