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

    [HttpPost()]
    public async Task<IActionResult> PostAsync(Country entity)
    {
        var result = await _countryService.AddAsync(entity);
        if (!result.IsSuccess) return BadRequest(result.Message);
        return new ObjectResult(result.Result!) { StatusCode = StatusCodes.Status201Created };
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _countryService.DeleteAsync(id);
        if (result.IsSuccess) return NoContent();
        return BadRequest(result.Message);
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

    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetAsync(int id)
    {
        var response = await _countryService.GetByIdAsync(id);
        if (response.IsSuccess) return Ok(response.Result);
        return BadRequest(response.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, Country entity)
    {
        if (id != entity.CountryId) return BadRequest("Id del País no son iguales");
        var response = await _countryService.UpdateAsync(entity);
        if (!response.IsSuccess) return BadRequest(response.Message);
        return NoContent();
    }
}