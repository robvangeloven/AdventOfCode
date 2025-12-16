namespace AdventOfCode.Runner;

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

internal class PuzzleInputHelper
{
    private IConfiguration _configuration;

    public PuzzleInputHelper(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private static string GetInputPath(int year, int day, [CallerFilePath] string? path = null) => $"{Path.GetDirectoryName(path)}/PuzzleInputs/{year}/{day}.txt";

    private static async Task DownloadInput(int year, int day, string sessionToken, string downloadPath)
    {
        Console.WriteLine("Fetching input...");

        using (var handler = new HttpClientHandler { UseCookies = false })
        {
            using var client = new HttpClient(handler);

            var message = new HttpRequestMessage(HttpMethod.Get, $"https://adventofcode.com/{year}/day/{day}/input");
            message.Headers.Add("Cookie", $"session={sessionToken}");
            message.Headers.TryAddWithoutValidation(HeaderNames.UserAgent, "github.com/robvangeloven/AdventOfCode by rob@xprtz.net");
            var result = await client.SendAsync(message);

            try
            {
                result.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new NotSupportedException($"Couldn't get problem input, maybe your session token is old. Please manually provide the problem input at \"\\Inputs\\Day{day}.txt\"", ex);
            }

            var responseBody = await result.Content.ReadAsStringAsync();

            Directory.CreateDirectory(Path.GetDirectoryName(downloadPath)!);

            File.WriteAllText(downloadPath, responseBody.ReplaceLineEndings().TrimEnd());
        }

        Console.WriteLine("Input successfully fetched");
    }

    public async Task<string> GetPuzzleInput(int year, int day)
    {
        var path = GetInputPath(year, day);

        if (!File.Exists(path))
        {
            var sessionToken = _configuration.GetValue<string>("SessionToken") ?? throw new NotSupportedException($"No session token available to get input. Please manually provide the problem input at \"\\Inputs\\Day.txt\"");
            await DownloadInput(year, day, sessionToken, path);
        }

        return await File.ReadAllTextAsync(path);
    }
}
