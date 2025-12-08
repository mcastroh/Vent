using System.ComponentModel.DataAnnotations;

namespace Vent.Shared.Entities;

public class City
{
    [Key]
    public int CityId { get; set; }

    [Display(Name = "Ciudad")]
    [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Name { get; set; } = null!;

    public int StateId { get; set; }
    public virtual State? State { get; set; }
}