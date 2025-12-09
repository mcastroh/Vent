using System.ComponentModel.DataAnnotations;
using Vent.Shared.Resources;

namespace Vent.Shared.Entities;

public class Country
{
    [Key]
    public int CountryId { get; set; }

    [Display(Name = "Country", ResourceType = typeof(Resource))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
    public string Name { get; set; } = null!;

    [Display(Name = "CodePhone", ResourceType = typeof(Resource))]
    [MaxLength(10, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resource))]
    public string? CodePhone { get; set; }

    [Display(Name = "IsActive", ResourceType = typeof(Resource))]
    public bool IsActive { get; set; }

    [Display(Name = "Flag", ResourceType = typeof(Resource))]
    public string? CountryFlag { get; set; }

    [Display(Name = "Total", ResourceType = typeof(Resource))]
    public int StatesNumber => States == null ? 0 : States.Count;

    public string CountryImageFull => string.IsNullOrEmpty(CountryFlag) ? "/images/NoImage.png" : CountryFlag;

    public virtual ICollection<State>? States { get; set; }
}