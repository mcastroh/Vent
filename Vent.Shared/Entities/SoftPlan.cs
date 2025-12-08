using System.ComponentModel.DataAnnotations;

namespace Vent.Shared.Entities;

public class SoftPlan
{
    [Key]
    public int SoftPlanId { get; set; }

    [Display(Name = "SoftPlan")]
    [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Name { get; set; } = null!;

    public int CountItem { get; set; }

    public decimal Price { get; set; }

    public int TimeMonth { get; set; }

    public bool Active { get; set; }

    [Display(Name = "Fecha Auditoría")]
    public DateTime AuditoriaFecha { get; set; }
}