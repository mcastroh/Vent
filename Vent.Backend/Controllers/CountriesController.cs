using Microsoft.AspNetCore.Mvc;
using Vent.Services.Interfaces;
using Vent.Shared.Dtos;
using Vent.Shared.Entities;

namespace Vent.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetAsync([FromQuery] PaginationDto pagination)
    {
        var response = await _countryService.GetAsync(pagination);

        if (response.IsSuccess)
        {
            var lista = (List<Country>)response.Result!;
            Response.Headers.Append("conteo", response.CountItem.ToString());
            return Ok(lista);
        }

        return BadRequest("Error de Lectura.");
    }
}