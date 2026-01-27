using DoggyApi.Models;
using DoggyApi.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ICryptoService, CryptoService>();

var app = builder.Build();
app.MapGet("/", () => "DoggyApi is running ✅ Try /dogs");
// POST /encrypt
app.MapPost("/encrypt", (CryptoRequest request, ICryptoService crypto) =>
{
    var result = crypto.Encrypt(request.Text, request.Shift);
    return Results.Ok(new CryptoResponse(result));
});

// POST /decrypt
app.MapPost("/decrypt", (CryptoRequest request, ICryptoService crypto) =>
{
    var result = crypto.Decrypt(request.Text, request.Shift);
    return Results.Ok(new CryptoResponse(result));
});



// In-memory list (temporary "database")
var dogs = new List<Dog>
{
    new Dog(1, "Luna", "tax", false),
    new Dog(2, "Laura", "tax", false),
};

// GET /dogs - get all dogs
app.MapGet("/dogs", () => Results.Ok(dogs));

// GET /dogs/{id} - get one dog
app.MapGet("/dogs/{id:int}", (int id) =>
{
    var dog = dogs.FirstOrDefault(d => d.Id == id);
    return dog is null ? Results.NotFound() : Results.Ok(dog);
});

// POST /dogs - add a new dog
app.MapPost("/dogs", ([FromBody] CreateDogDto dto) =>
{
    var newId = dogs.Count == 0 ? 1 : dogs.Max(d => d.Id) + 1;

    var newDog = new Dog(
        newId,
        dto.Name,
        dto.Breed,
        dto.Present
    );

    dogs.Add(newDog);

    return Results.Created($"/dogs/{newId}", newDog);
});

// PATCH /dogs/{id} - update present (true/false)
app.MapPatch("/dogs/{id:int}", (int id, [FromBody] UpdateDogPresentDto dto) =>
{
    var dog = dogs.FirstOrDefault(d => d.Id == id);
    if (dog is null) return Results.NotFound();

    var updatedDog = dog with { Present = dto.Present };

    var index = dogs.FindIndex(d => d.Id == id);
    dogs[index] = updatedDog;

    return Results.Ok(updatedDog);
});

// DELETE /dogs/{id} - delete a dog
app.MapDelete("/dogs/{id:int}", (int id) =>
{
    var dog = dogs.FirstOrDefault(d => d.Id == id);
    if (dog is null) return Results.NotFound();

    dogs.Remove(dog);
    return Results.NoContent();
});

app.Run();

public record Dog(int Id, string Name, string Breed, bool Present);

// DTOs (what client sends)
public record CreateDogDto(string Name, string Breed, bool Present);
public record UpdateDogPresentDto(bool Present);
