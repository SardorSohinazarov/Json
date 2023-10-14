using Json_2.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () =>
{
    var person = new Person
    {
        Name = "Sardor",
        Address = "Farg'ona",
        Country = "Uzbekistan",
        Phone = "+998912040618"
    };

    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };

    return Results.Json(
        person,options, "json",200
    );
});

app.Run();
