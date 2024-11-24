using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RFDSTakeHomeProblem.Models;
using RFDSTakeHomeProblem.Services;
using RFDSTakeHomeProblem.Interfaces;
using RFDSTakeHomeProblem.Exceptions;

namespace RFDSTakeHomeProblem;

public class Program
{
    public static void Main(string[] args)
    {
        ServiceProvider serviceProvider = ConfigureServices();

        using IServiceScope scope = serviceProvider.CreateScope();
        ICipherService cipherService = scope.ServiceProvider.GetRequiredService<ICipherService>();
        ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            Console.WriteLine("RFDS Caesar Cipher Decryption Tool!");
            Console.WriteLine("Enter the encrypted text (lowercase letters only):");

            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input provided. Exiting...");
                return;
            }

            string decrypted = cipherService.Decrypt(input);
            Console.WriteLine($"Decrypted text: {decrypted}");
        }
        catch (CipherException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            logger.LogError(ex, "Decryption failed");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred.");
            logger.LogError(ex, "An unexpected error occurred");
        }
    }

    private static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            })
            .AddSingleton(new CipherSettings())
            .AddScoped<ICipherService, CipherService>()
            .BuildServiceProvider();
    }
}