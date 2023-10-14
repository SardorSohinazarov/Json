using Json_1.Models;

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

    return Results.Json(person);
});

app.Run();
