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


// Service registration
builder.Services.AddSingleton<ICryptoService, CryptoService>();

var app = builder.Build();

// Health/status endpoint
app.MapGet("/", () => "Crypto API is running ✅ Try POST /encrypt and POST /decrypt");

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


// ===== Models =====
public record CryptoRequest(string Text, int Shift);
public record CryptoResponse(string Result);


// ===== Service =====
public interface ICryptoService
{
    string Encrypt(string text, int shift);
    string Decrypt(string text, int shift);
}

public class CryptoService : ICryptoService
{
    public string Encrypt(string text, int shift) => Caesar(text, shift);
    public string Decrypt(string text, int shift) => Caesar(text, -shift);

    private static string Caesar(string text, int shift)
    {
        if (string.IsNullOrEmpty(text)) return "";

        shift %= 26;

        char ShiftChar(char c, char a)
        {
            int offset = c - a;
            int shifted = (offset + shift) % 26;
            if (shifted < 0) shifted += 26;
            return (char)(a + shifted);
        }

        var chars = text.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            var c = chars[i];

            if (c >= 'a' && c <= 'z') chars[i] = ShiftChar(c, 'a');
            else if (c >= 'A' && c <= 'Z') chars[i] = ShiftChar(c, 'A');
            // else keep as-is (spaces, numbers, symbols)
        }

        return new string(chars);
    }
}

