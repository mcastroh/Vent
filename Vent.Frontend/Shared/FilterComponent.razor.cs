using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Vent.Shared.Resources;

namespace Vent.Frontend.Shared;

public partial class FilterComponent
{
    [Inject] private IStringLocalizer<Resource> Localizer { get; set; } = null!;

    [Parameter] public EventCallback<string> ApplyFilter { get; set; }
    [Parameter] public string FilterValue { get; set; } = string.Empty;

    private async Task CleanFilter()
    {
        FilterValue = string.Empty;
        await ApplyFilter.InvokeAsync(FilterValue);
    }

    private async Task OnFilterApply()
    {
        await ApplyFilter.InvokeAsync(FilterValue);
    }
}