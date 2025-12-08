using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Vent.Shared.Resources;

namespace Vent.Frontend.Shared;

public partial class Loading
{
    [Parameter] public string? Label { get; set; }
    [Inject] private IStringLocalizer<Resource> Localizer { get; set; } = null!;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (string.IsNullOrEmpty(Label)) Label = @Localizer["PleaseWait"];
    }
}