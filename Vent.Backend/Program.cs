using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Vent.Backend.Data;
using Vent.Backend.Data.LoadCountries;
using Vent.DataAccess;
using Vent.Repositories.Implementations;
using Vent.Repositories.Interfaces;
using Vent.Services.Implementations;
using Vent.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Arquitectura Limpia - Ventas",
        Version = "v1"
    });
});

//opt.UseLazyLoadingProxies();

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("CnLocalConnectionSqlServer"),
        sqlServerOptions => sqlServerOptions.CommandTimeout(0)
        );
});

builder.Services.AddTransient<SeedDb>();

builder.Services.AddScoped<ICountryRepository, CountryRepository>();

builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddScoped<IApiService, ApiService>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", builder =>
//    {
//        builder.WithOrigins("https://localhost:7208/")  // dominio App Blazor  AllowAnyOrigin()
//               .AllowAnyHeader()
//               .AllowAnyMethod()
//               //.SetIsOriginAllowed(origin => true)
//               .WithExposedHeaders(new string[] { "Totalpages", "conteo" });
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed(origin => true)
               .WithExposedHeaders(new string[] { "Totalpages", "conteo" });
    });
});

var app = builder.Build();

SeedData(app);

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using var scope = scopedFactory!.CreateScope();
    var service = scope.ServiceProvider.GetService<SeedDb>();
    service!.SeedAsync().Wait();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html", permanent: false);
        return;
    }
    await next();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();