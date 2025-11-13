using System.ComponentModel.DataAnnotations;

namespace GerenciadorReceitas.Models;

public class Favorito
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "O IdMeal é obrigatório")]
    public string IdMeal { get; set; }

    [Required(ErrorMessage = "O título é obrigatório")]
    public string Title { get; set; }
}
