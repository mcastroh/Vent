using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Vent.Shared.Resources;

namespace Vent.Frontend.Shared;

public partial class InputImg
{
    private string? imageBase64;
    private string? fileName;

    [Parameter] public string? Label { get; set; }
    [Parameter] public string? ImageURL { get; set; }
    [Parameter] public EventCallback<string> ImageSelected { get; set; }

    [Inject] private IStringLocalizer<Resource> Localizer { get; set; } = null!;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (string.IsNullOrWhiteSpace(Label)) Label = "Imagen";
    }

    private async Task OnChange(InputFileChangeEventArgs e)
    {
        var file = e.File;

        if (file != null)
        {
            fileName = file.Name;

            var arrBytes = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(arrBytes);
            imageBase64 = Convert.ToBase64String(arrBytes);
            ImageURL = null;
            await ImageSelected.InvokeAsync(imageBase64);
            StateHasChanged();
        }
    }
}