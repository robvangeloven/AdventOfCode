using AdventOfCode.DependencyInjection;
using AdventOfCode.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var year = 2024;
var day = 17;

var host = Host.CreateApplicationBuilder(args);

host.Configuration.AddUserSecrets<Program>();

host.Services.AddSingleton<PuzzleInputHelper>();
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
