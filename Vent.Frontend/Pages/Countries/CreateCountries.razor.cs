using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using Vent.Frontend.Repositories;
using Vent.Shared.Entities;
using Vent.Shared.Resources;

namespace Vent.Frontend.Pages.Countries;

public partial class CreateCountries
{
    private FormCountry? FormCountry;
    private Country Country = new() { IsActive = true };

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IStringLocalizer<Resource> Localizer { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/countries", Country);

        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[message!], Severity.Error);
            return;
        }

        Return();
        Snackbar.Add(Localizer["RecordCreatedOk"], Severity.Success);
    }

    private void Return()
    {
        FormCountry!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/countries");
    }
}