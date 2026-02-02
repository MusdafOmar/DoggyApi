using DoggyApi.Services;
using Xunit;

namespace DoggyApi.Tests;

public class CryptoServiceTests
{
    private readonly ICryptoService _cryptoService;

    public CryptoServiceTests()
    {
        _cryptoService = new CryptoService();
    }

    [Fact]
    public void Encrypt_WithShift3_ShouldEncryptHelloToKhoor()
    {
        // Arrange
        var input = "Hello";
        var shift = 3;

        // Act
        var result = _cryptoService.Encrypt(input, shift);

        // Assert
        Assert.Equal("Khoor", result);
    }

    [Fact]
    public void Decrypt_WithShift3_ShouldDecryptKhoorToHello()
    {
        // Arrange
        var encrypted = "Khoor";
        var shift = 3;

        // Act
        var result = _cryptoService.Decrypt(encrypted, shift);

        // Assert
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void Encrypt_WithZeroShift_ReturnsSameText()
    {
        // Arrange
        var input = "Hello";
        var shift = 0;

        // Act
        var result = _cryptoService.Encrypt(input, shift);

        // Assert
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void Encrypt_ShouldHandleUpperAndLowerCase()
    {
        // Arrange
        var input = "AbC";
        var shift = 1;

        // Act
        var result = _cryptoService.Encrypt(input, shift);

        // Assert
        Assert.Equal("BcD", result);
    }

    [Fact]
    public void Encrypt_ShouldKeepNonLettersUnchanged()
    {
        // Arrange
        var input = "Hello, 123!";
        var shift = 1;

        // Act
        var result = _cryptoService.Encrypt(input, shift);

        // Assert
        Assert.Equal("Ifmmp, 123!", result);
    }
}
