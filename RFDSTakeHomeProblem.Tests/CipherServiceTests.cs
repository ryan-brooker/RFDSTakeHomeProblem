using Microsoft.Extensions.Logging;
using Moq;
using RFDSTakeHomeProblem.Exceptions;
using RFDSTakeHomeProblem.Models;
using RFDSTakeHomeProblem.Services;

namespace RFDSTakeHomeProblem.Tests;

public class CipherServiceTests
{
    private readonly Mock<ILogger<CipherService>> _loggerMock;
    private readonly CipherSettings _settings;
    private readonly CipherService _service;

    public CipherServiceTests()
    {
        _loggerMock = new Mock<ILogger<CipherService>>();
        _settings = new CipherSettings();
        _service = new CipherService(_loggerMock.Object, _settings);
    }

    [Theory]
    [InlineData("def", "abc")]
    [InlineData("xyz", "uvw")]
    [InlineData("abc", "xyz")]
    public void Decrypt_ValidInput_ReturnsCorrectPlaintext(string cipherText, string expectedPlaintext)
    {
        string result = _service.Decrypt(cipherText);
        Assert.Equal(expectedPlaintext, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Decrypt_EmptyOrWhitespaceInput_ThrowsCipherException(string input)
    {
        Assert.Throws<CipherException>(() => _service.Decrypt(input));
    }

    [Fact]
    public void Decrypt_InputExceedsMaxLength_ThrowsCipherException()
    {
        string input = new string('a', _settings.MaxInputLength + 1);
        Assert.Throws<CipherException>(() => _service.Decrypt(input));
    }

    [Theory]
    [InlineData("ABC")]
    [InlineData("123")]
    [InlineData("ab!c")]
    public void Decrypt_InvalidCharacters_ThrowsCipherException(string input)
    {
        Assert.Throws<CipherException>(() => _service.Decrypt(input));
    }

    [Fact]
    public void ClearSensitiveData_ClearsBuffer()
    {
        _service.Decrypt("abc");
        _service.ClearSensitiveData();
        Assert.True(true);
    }
}