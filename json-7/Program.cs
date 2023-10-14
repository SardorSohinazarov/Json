using System.Text.Json;
using System.Text.Json.Nodes;
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
        Phone = new List<string>
        {
            "+998912040618",
            "+998908563748",
            "+998911418502",
        }
    };

    var jsonObjects = new JsonObject()
    {
        ["name"] = JsonValue.Create(person.Name),
        ["address"] = JsonValue.Create(person.Address),
        ["country"] = JsonValue.Create(person.Country),
        //["phone"] = JsonValue.Create(person.Phone)
        ["phone"] = new JsonArray()
        {
            JsonValue.Create(person.Phone)
        }
    };

    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };

    return Results.Json(
        jsonObjects, options, "json", 200
    );
});

app.Run();


public class Person
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Address { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Country { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public List<string> Phone { get; set; }
}
