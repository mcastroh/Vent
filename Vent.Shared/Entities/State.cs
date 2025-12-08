using System.ComponentModel.DataAnnotations;

namespace Vent.Shared.Entities;

public class State
{
    [Key]
    public int StateId { get; set; }

    [Display(Name = "Departamento/Estado")]
    [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Name { get; set; } = null!;

    public int CountryId { get; set; }
    public virtual Country? Country { get; set; }

    [Display(Name = "Total Ciudades")]
    public int CitiesNumber => Cities == null ? 0 : Cities.Count;

    public virtual ICollection<City>? Cities { get; set; }
}