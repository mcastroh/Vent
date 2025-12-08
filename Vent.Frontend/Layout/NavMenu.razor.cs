using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Vent.Shared.Resources;

namespace Vent.Frontend.Layout;

public partial class NavMenu
{
    private bool collapseNavMenu = true;
    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    [Inject] private IStringLocalizer<Resource> Localizer { get; set; } = null!;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }
}