namespace DoggyApi.Services;

public interface ICryptoService
{
    string Encrypt(string text, int shift);
    string Decrypt(string text, int shift);
}

public class CryptoService : ICryptoService
{
    public string Encrypt(string text, int shift)
        => Caesar(text, shift);

    public string Decrypt(string text, int shift)
        => Caesar(text, -shift);

    private static string Caesar(string text, int shift)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        shift %= 26;
        char Shift(char c, char a)
            => (char)(a + (c - a + shift + 26) % 26);

        var chars = text.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] >= 'a' && chars[i] <= 'z')
                chars[i] = Shift(chars[i], 'a');
            else if (chars[i] >= 'A' && chars[i] <= 'Z')
                chars[i] = Shift(chars[i], 'A');
        }

        return new string(chars);
    }
}
