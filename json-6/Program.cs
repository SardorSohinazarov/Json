using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () =>
{
    var jsonNode = JsonNode.Parse(@"
    [
        {
            ""name"" : ""anne"",
            ""age"" : 34,
            ""gender"" : ""female""
        },
        {
            ""name"" : ""hadi"",
            ""age"" : 29,
            ""gender"" : ""non-binary"",
            ""favoriteNumbers"" : [1, 5, 6] 
        },
        {
            ""name"" : ""abdelfattah"",
            ""age"" : 30,
            ""gender"" : ""non-binary"",
            ""favoriteNumbers"" : [3, 9, 10, 11] 
        }
    ]");

    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };

    return Results.Json(
        jsonNode, options, "json", 200
    );
});

app.Run();