namespace AdventOfCode.Runner;

using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;

internal class PuzzleInputHelper
{
    private readonly HttpClient _httpClient;

    public PuzzleInputHelper(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    private static string GetInputPath(int year, int day, [CallerFilePath] string? path = null) => $"{Path.GetDirectoryName(path)}/PuzzleInputs/{year}/{day}.txt";

    private async Task DownloadInput(int year, int day, string downloadPath)
    {
        var inputUrl = $"/{year}/day/{day}/input";

        Console.WriteLine($"Downloading puzzle input from '{inputUrl}'.");

        var result = await _httpClient.GetAsync(inputUrl);

        try
        {
            result.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new NotSupportedException($"Couldn't get problem input, maybe the session token expired. Please provide the problem input at '{GetInputPath(year, day)}'.", ex);
        }

        var responseBody = await result.Content.ReadAsStringAsync();

        Directory.CreateDirectory(Path.GetDirectoryName(downloadPath)!);

        File.WriteAllText(downloadPath, responseBody.ReplaceLineEndings().TrimEnd());

        Console.WriteLine("Puzzle input successfully fetched.");
    }

    public async Task<string> GetPuzzleInput(int year, int day)
    {
        var path = GetInputPath(year, day);

        if (!File.Exists(path))
        {   
            await DownloadInput(year, day, path);
        }

        return await File.ReadAllTextAsync(path);
    }
}
