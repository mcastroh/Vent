using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using Vent.Shared.Entities;
using Vent.Shared.Resources;

namespace Vent.Frontend.Pages.Countries;

public partial class FormCountry
{
    private EditContext editContext = null!;

    [EditorRequired, Parameter] public Country Country { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

    [Inject] private IStringLocalizer<Resource> Localizer { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    public bool FormPostedSuccessfully { get; set; } = false;
    private bool primeraVez = true;

    private string? isActiveMessage;
    private string? imageUrl;

    protected override void OnInitialized()
    {
        editContext = new(Country);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (!string.IsNullOrEmpty(Country.CountryFlag)) imageUrl = Country.CountryFlag;
        isActiveMessage = Country.IsActive ? $"{Localizer["Country"]} {Localizer["Active"]}" : $"{Localizer["Country"]} {Localizer["Inactive"]}";
    }

    private async Task OnBeforeInternalNavigation(LocationChangingContext context)
    {
        var formWasEdited = editContext.IsModified();
        if (!formWasEdited || FormPostedSuccessfully) return;

        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = Localizer["Confirmation"],
            Text = Localizer["LeaveAndLoseChanges"],
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true,
            CancelButtonText = Localizer["Cancel"]
        });

        var confirm = !string.IsNullOrEmpty(result.Value);
        if (confirm) return;
        context.PreventNavigation();
    }

    private void ImageSelected(string imagenBase64)
    {
        Country.CountryFlag = imagenBase64;
        imageUrl = null;
        primeraVez = false;
    }

    private void OnToggledChanged(bool toggled)
    {
        Country.IsActive = toggled;
        isActiveMessage = Country.IsActive ? $"{Localizer["Country"]} {Localizer["Active"]}" : $"{Localizer["Country"]} {Localizer["Inactive"]}";
    }
}