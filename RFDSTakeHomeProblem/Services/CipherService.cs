using Microsoft.Extensions.Logging;
using RFDSTakeHomeProblem.Exceptions;
using RFDSTakeHomeProblem.Interfaces;
using RFDSTakeHomeProblem.Models;
using System.Text;

namespace RFDSTakeHomeProblem.Services;

public class CipherService : ICipherService
{
    private readonly ILogger<CipherService> _logger;
    private readonly CipherSettings _settings;
    private StringBuilder? _sensitiveDataBuffer;

    public CipherService(ILogger<CipherService> logger, CipherSettings settings)
    {
        _logger = logger;
        _settings = settings;
    }

    public string Decrypt(string cipherText)
    {
        try
        {
            ValidateInput(cipherText);

            _sensitiveDataBuffer = new StringBuilder(cipherText.Length);

            foreach (char c in cipherText)
            {
                if (!char.IsLetter(c) || !char.IsLower(c))
                {
                    throw new CipherException("Invalid character detected. Only lowercase letters are allowed.");
                }

                char decrypted = DecryptChar(c);
                _sensitiveDataBuffer.Append(decrypted);
            }

            string result = _sensitiveDataBuffer.ToString();
            ClearSensitiveData();
            return result;
        }
        catch (Exception ex) when (ex is not CipherException)
        {
            _logger.LogError(ex, "An error occurred during decryption");
            throw new CipherException("Decryption failed", ex);
        }
    }

    private void ValidateInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new CipherException("Input cannot be empty or whitespace");
        }

        if (input.Length > _settings.MaxInputLength)
        {
            throw new CipherException($"Input length exceeds maximum allowed length of {_settings.MaxInputLength} chars");
        }
    }

    private char DecryptChar(char c)
    {
        int shift = _settings.DefaultShift;

        // The decryption formula works as follows:
        // (c - 'a') will convert to 0 base pos
        // (26 - shift) calculates the reverse shift
        // % 26 need this for wrap around clause
        // + 'a' convert back to ASCII 
        return (char)(((c - 'a' + (26 - shift)) % 26) + 'a');
    }

    public void ClearSensitiveData()
    {
        // Clear 'sensitive' data from memory post decryption
        if (_sensitiveDataBuffer != null)
        {
            // Overwrite the buffer with zeros
            for (int i = 0; i < _sensitiveDataBuffer.Length; i++)
            {
                _sensitiveDataBuffer[i] = '\0';
            }

            // Clearing then a null ref is probably overkill
            _sensitiveDataBuffer.Clear();
            _sensitiveDataBuffer = null;
        }
    }
}
