namespace AdventOfCode.Solutions._2024;

using System.Threading.Tasks;
using AdventOfCode.Attributes;
using AdventOfCode.Tools.Map;
using AdventOfCode.Tools.Map.MapRunner;

[AdventOfCodeDay(2024, 12)]
internal class Day12 : IAdventOfCodeDay
{
    private Map<char> map = null!;

    public async Task Setup(string puzzleInput)
    {
        map = Map<char>.Load(puzzleInput);
    }

    public async Task<string> SolvePart1()
    {
        var answer = 0;

        map.TryGetValue(0, 0, out var tile);

        var runner = new RegionRunner<char>(tile, map);

        runner.Run();

        answer = runner.Regions.Sum(region => region.Area * region.Perimiter);

        // high: 1433458

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = 0;

        return $"{answer}";
    }
}
