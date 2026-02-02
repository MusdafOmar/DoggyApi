namespace DoggyApi.Services
{
    public interface ICryptoService
    {
        string Encrypt(string text, int shift);
        string Decrypt(string text, int shift);
    }

    public class CryptoService : ICryptoService
    {
        public string Encrypt(string text, int shift) => Caesar(text, shift);

        public string Decrypt(string text, int shift) => Caesar(text, -shift);

        private static string Caesar(string input, int shift)
        {
            if (string.IsNullOrEmpty(input)) return input;

            shift = shift % 26;

            char ShiftChar(char c, char baseChar)
            {
                int offset = c - baseChar;
                int newOffset = (offset + shift) % 26;
                if (newOffset < 0) newOffset += 26;
                return (char)(baseChar + newOffset);
            }

            var chars = input.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];

                if (c >= 'A' && c <= 'Z')
                    chars[i] = ShiftChar(c, 'A');
                else if (c >= 'a' && c <= 'z')
                    chars[i] = ShiftChar(c, 'a');
                // else: keep spaces, numbers, symbols unchanged
            }

            return new string(chars);
        }
    }
}
