using System.ComponentModel.DataAnnotations;

namespace Vent.Shared.Entities;

public class Country
{
    [Key]
    public int CountryId { get; set; }

    [Display(Name = "País")]
    [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Name { get; set; } = null!;

    [MaxLength(10, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
    [Display(Name = "Código Teléfono")]
    public string? CodePhone { get; set; }

    [Display(Name = "Total Departamentos/Estados")]
    public int StatesNumber => States == null ? 0 : States.Count;

    public virtual ICollection<State>? States { get; set; }
}