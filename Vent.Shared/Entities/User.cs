using System.ComponentModel.DataAnnotations;

namespace Vent.Shared.Entities;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Display(Name = "Usuario")]
    [MaxLength(60, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Name { get; set; } = null!;
}