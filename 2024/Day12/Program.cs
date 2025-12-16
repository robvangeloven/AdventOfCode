using AdventOfCode.Tools.Map;
using AdventOfCode.Tools.Map.MapRunner;

var map = Map<char>.Load("input.txt");

void PartOne()
{
    var answer = 0;

    map.TryGetValue(0, 0, out var tile);

    var runner = new RegionRunner<char>(tile, map);

    runner.Run();

    answer = runner.Regions.Sum(region => region.Area * region.Perimiter);

    // high: 1433458
    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    var answer = 0;

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();
