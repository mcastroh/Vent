using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using Vent.Shared.Resources;

namespace Vent.Frontend.Shared;

public partial class ConfirmDialog
{
    [Inject] private IStringLocalizer<Resource> Localizer { get; set; } = null!;
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public string Message { get; set; } = null!;

    private void Accept()
    {
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel()
    {
        MudDialog.Close(DialogResult.Cancel());
    }
}