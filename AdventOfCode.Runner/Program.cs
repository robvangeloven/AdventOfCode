using AdventOfCode.DependencyInjection;
using AdventOfCode.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

var year = 2025;
var day = 3;

var host = Host.CreateApplicationBuilder(args);

host.Configuration.AddUserSecrets<Program>();

host.Services.AddHttpClient<PuzzleInputHelper>(client =>
{
    var sessionToken = host.Configuration.GetValue<string>("SessionToken") ?? throw new NotSupportedException($"No session token available to get puzzle input.");

    client.BaseAddress = new Uri("https://adventofcode.com");
    client.DefaultRequestHeaders.Add(HeaderNames.Cookie, $"session={sessionToken}");
    client.DefaultRequestHeaders.TryAddWithoutValidation(HeaderNames.UserAgent, "github.com/robvangeloven/AdventOfCode by rob@xprtz.net");
});

host.Services.AddScoped<PuzzleInputHelper>();
host.Services.AddAdventOfCodeDays();

var app = host.Build();

await using var scope = app.Services.CreateAsyncScope();

var puzzleInputHelper = scope.ServiceProvider.GetRequiredService<PuzzleInputHelper>();

var input = await puzzleInputHelper.GetPuzzleInput(year, day);

var adventOfCodeDay = scope.GetAdventOfCodeDay(year, day);

await adventOfCodeDay.Setup(input);

var answer = await adventOfCodeDay.SolvePart1();

Console.WriteLine($"The answer for part 1: {answer}");

answer = await adventOfCodeDay.SolvePart2();

Console.WriteLine($"The answer for part 2: {answer}");

Console.ReadKey();
