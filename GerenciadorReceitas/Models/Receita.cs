using System.ComponentModel.DataAnnotations;

namespace GerenciadorReceitas.Models;

public class Receita
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "O título é obrigatório")]
    [MinLength(3, ErrorMessage = "O título deve ter pelo menos 3 caracteres")]
    public string Title { get; set; }

    [Required(ErrorMessage = "É necessário informar os ingredientes")]
    public List<string> Ingredients { get; set; }

    [Required(ErrorMessage = "As instruções são obrigatórias")]
    public string Instructions { get; set; }

    public string? Image { get; set; }
}
