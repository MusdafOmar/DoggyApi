using DoggyApi.Services;
using Xunit;

namespace DoggyApi.Tests;

public class CryptoServiceTests
{
    [Fact]
    public void Encrypt_Then_Decrypt_Returns_Original_Text()
    {
        // Arrange
        ICryptoService crypto = new CryptoService();
        var original = "Hello World";
        var shift = 3;

        // Act
        var encrypted = crypto.Encrypt(original, shift);
        var decrypted = crypto.Decrypt(encrypted, shift);

        // Assert
        Assert.Equal(original, decrypted);
    }
}
