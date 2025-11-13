using Microsoft.AspNetCore.Mvc;
using GerenciadorReceitas.Models;
using System.Text.Json;

namespace GerenciadorReceitas.Controllers;

[ApiController]
[Route("local")]
public class LocalController : ControllerBase
{
    private readonly string receitasPath = "Data/receitas.json";

    private List<Receita> CarregarReceitas()
    {
        if (!System.IO.File.Exists(receitasPath)) return new();
        return JsonSerializer.Deserialize<List<Receita>>(System.IO.File.ReadAllText(receitasPath)) ?? new();
    }

    private void SalvarReceitas(List<Receita> receitas)
    {
        System.IO.File.WriteAllText(receitasPath, JsonSerializer.Serialize(receitas, new JsonSerializerOptions { WriteIndented = true }));
    }

    /// <summary>
    /// </summary>
    [HttpGet("receitas")]
    public IActionResult Listar()
    {
        try
        {
            return Ok(CarregarReceitas());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao carregar receitas", error = ex.Message });
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="receita">Objeto JSON da receita</param>
    [HttpPost("receitas")]
    public IActionResult Criar([FromBody] Receita receita)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var receitas = CarregarReceitas();
            receitas.Add(receita);
            SalvarReceitas(receitas);
            return Ok(receita);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao salvar receita", error = ex.Message });
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="id">ID da receita</param>
    /// <param name="receitaAtualizada">Objeto JSON atualizado</param>
    [HttpPut("receitas/{id}")]
    public IActionResult Editar(string id, [FromBody] Receita receitaAtualizada)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var receitas = CarregarReceitas();
            var receita = receitas.FirstOrDefault(r => r.Id == id);
            if (receita == null) return NotFound(new { message = "Receita não encontrada" });

            receita.Title = receitaAtualizada.Title;
            receita.Ingredients = receitaAtualizada.Ingredients;
            receita.Instructions = receitaAtualizada.Instructions;
            receita.Image = receitaAtualizada.Image;

            SalvarReceitas(receitas);
            return Ok(new { message = "Receita atualizada com sucesso" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao atualizar receita", error = ex.Message });
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="id">ID da receita</param>
    [HttpDelete("receitas/{id}")]
    public IActionResult Excluir(string id)
    {
        try
        {
            var receitas = CarregarReceitas().Where(r => r.Id != id).ToList();
            SalvarReceitas(receitas);
            return Ok(new { message = "Receita excluída com sucesso" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao excluir receita", error = ex.Message });
        }
    }
}
