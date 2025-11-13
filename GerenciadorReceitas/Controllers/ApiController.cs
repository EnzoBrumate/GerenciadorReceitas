using Microsoft.AspNetCore.Mvc;
using GerenciadorReceitas.Models;
using GerenciadorReceitas.Services;
using System.Text.Json;

namespace GerenciadorReceitas.Controllers;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    private readonly MealDbService _service;
    private readonly string favPath = "Data/favoritos.json";

    public ApiController(MealDbService service)
    {
        _service = service;
    }

    private List<Favorito> CarregarFavoritos()
    {
        if (!System.IO.File.Exists(favPath)) return new();
        return JsonSerializer.Deserialize<List<Favorito>>(System.IO.File.ReadAllText(favPath)) ?? new();
    }

    private void SalvarFavoritos(List<Favorito> favoritos)
    {
        System.IO.File.WriteAllText(favPath, JsonSerializer.Serialize(favoritos, new JsonSerializerOptions { WriteIndented = true }));
    }

    /// <summary>
    /// </summary>
    /// <param name="nome">Nome da receita</param>
    [HttpGet("buscar-nome")]
    public async Task<IActionResult> BuscarPorNome([FromQuery] string nome)
    {
        try
        {
            var meals = await _service.BuscarPorNome(nome);
            if (meals.Count == 0) return NotFound(new { message = "Nenhuma receita encontrada" });
            return Ok(meals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar receitas", error = ex.Message });
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="ingrediente">Ingrediente da receita</param>
    [HttpGet("buscar-ingrediente")]
    public async Task<IActionResult> BuscarPorIngrediente([FromQuery] string ingrediente)
    {
        try
        {
            var meals = await _service.BuscarPorIngrediente(ingrediente);
            if (meals.Count == 0) return NotFound(new { message = "Nenhuma receita encontrada" });
            return Ok(meals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar receitas", error = ex.Message });
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="id">ID da receita</param>
    [HttpGet("buscar-id")]
    public async Task<IActionResult> BuscarPorId([FromQuery] string id)
    {
        try
        {
            var meal = await _service.BuscarPorId(id);
            if (meal == null) return NotFound(new { message = "Receita não encontrada" });
            return Ok(meal);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar receita", error = ex.Message });
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="fav">Objeto JSON com IdMeal e título</param>
    [HttpPost("favoritos")]
    public IActionResult Favoritar([FromBody] Favorito fav)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var favoritos = CarregarFavoritos();
            favoritos.Add(fav);
            SalvarFavoritos(favoritos);
            return Ok(new { message = "Favorito adicionado com sucesso" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao salvar favorito", error = ex.Message });
        }
    }

    /// <summary>
    /// </summary>
    [HttpGet("favoritos")]
    public IActionResult ListarFavoritos()
    {
        try
        {
            return Ok(CarregarFavoritos());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao carregar favoritos", error = ex.Message });
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="id">ID do favorito</param>
    [HttpDelete("favoritos/{id}")]
    public IActionResult RemoverFavorito(string id)
    {
        try
        {
            var favoritos = CarregarFavoritos().Where(f => f.Id != id).ToList();
            SalvarFavoritos(favoritos);
            return Ok(new { message = "Favorito removido com sucesso" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao remover favorito", error = ex.Message });
        }
    }
}
