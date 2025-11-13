using GerenciadorReceitas.Models;
using System.Net.Http.Json;

namespace GerenciadorReceitas.Services;

public class MealDbService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "https://www.themealdb.com/api/json/v1/1";

    public MealDbService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Meal>> BuscarPorNome(string nome)
    {
        var response = await _http.GetFromJsonAsync<Dictionary<string, List<Meal>>>($"{BaseUrl}/search.php?s={nome}");
        return response?["meals"] ?? new List<Meal>();
    }

    public async Task<List<Meal>> BuscarPorIngrediente(string ingrediente)
    {
        var response = await _http.GetFromJsonAsync<Dictionary<string, List<Meal>>>($"{BaseUrl}/filter.php?i={ingrediente}");
        return response?["meals"] ?? new List<Meal>();
    }

    public async Task<Meal?> BuscarPorId(string id)
    {
        var response = await _http.GetFromJsonAsync<Dictionary<string, List<Meal>>>($"{BaseUrl}/lookup.php?i={id}");
        return response?["meals"]?.FirstOrDefault();
    }
}
