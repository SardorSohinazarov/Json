using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", async context =>
{
    var person = new Person
    {
        Name = "Annie",
        Age = 33,
        IsMarried = false,
        CurrentTime = DateTimeOffset.UtcNow,
        Characters = new Dictionary<string, bool>
        {
                {"Funny" , true},
                {"Feisty" , true},
                {"Brilliant" , true},
                {"FOMA", false}
        },
        Superpowers = new List<Superpower> {
                new Superpower("Humor", 8),
                new Superpower("Intelligence", 10),
                new Superpower("Focus", 7)
        }
    };


    var options = new JsonWriterOptions
    {
        Indented = true,
    };

    context.Response.Headers.Add(HeaderNames.ContentType, "application/json");

    await using(var jsonwriter = new Utf8JsonWriter(context.Response.Body, options))
    {
        jsonwriter.WriteStartObject();
        jsonwriter.WriteString("name", person.Name);
        jsonwriter.WriteNumber("age", person.Age);
        jsonwriter.WriteBoolean("ismarried", person.IsMarried);
        jsonwriter.WriteString("currenttime", person.CurrentTime);

        jsonwriter.WriteStartObject("characters");
        foreach(var character in person.Characters)
        {
            jsonwriter.WriteBoolean(character.Key, character.Value);
        }
        
        jsonwriter.WriteStartObject("superpowers");
        foreach(var superpower in person.Superpowers)
        {
            jsonwriter.WriteString(superpower.Name, superpower.Rating.ToString());
        }

        jsonwriter.WriteEndObject();
    }
});

app.Run();


public class Superpower
{
    public string Name { get; set; }

    public short Rating { get; set; }

    public Superpower(string name, short rating)
    {
        Name = name;
        Rating = rating;
    }
}

public class Person
{
    public string Name { get; set; }

    public int Age { get; set; }

    public bool IsMarried { get; set; }

    public DateTimeOffset CurrentTime { get; set; }

    public Dictionary<string, bool> Characters { get; set; }

    public List<Superpower> Superpowers { get; set; }
}

