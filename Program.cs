using DoggyApi.Models;
using DoggyApi.Services;

var builder = WebApplication.CreateBuilder(args);
// Elastic Beanstalk sets PORT (example: 5000). Locally you might use 5047/https etc.
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}


// Register services
builder.Services.AddSingleton<ICryptoService, CryptoService>();

var app = builder.Build();

// Health/status endpoint
app.MapGet("/", () => "DoggyApi is running ✅ Try POST /encrypt and POST /decrypt");

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

app.Run();
