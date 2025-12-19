namespace AdventOfCode.Solutions._2025;

using System.Collections.Generic;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2025, 3)]
internal class Day3 : IAdventOfCodeDay
{
    private IEnumerable<string> _input = [];

    public async Task Setup(string puzzleInput)
    {
        _input = puzzleInput.Split(Environment.NewLine);
    }

    private static (int firstBattery, int secondBattery) FindBatteryPair(int battery, List<int> batteries, List<(int, int)> batteryPairs)
    {
        batteryPairs.Add((battery, batteries.First()));

        if (batteries.Count > 1)
        {
            foreach (var item in batteries)
            {
                return FindBatteryPair(batteries.First(), batteries[1..], batteryPairs);
            }
        }

        return batteryPairs.MaxBy(x => x.Item1 + x.Item2);
    }

    public async Task<string> SolvePart1()
    {
        var answer = _input.Sum(line =>
        {
            var batteries = line.Select(c => (int)char.GetNumericValue(c)).ToList();

            var firstBattery = batteries[..^1].Select((battery, index) => (battery, index)).MaxBy(battery => battery.battery);
            var secondBattery = batteries[(firstBattery.index + 1)..].Select((battery, index) => (battery, index)).MaxBy(battery => battery.battery);

            return (firstBattery.battery * 10) + secondBattery.battery;
        });

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = 0;

        return $"{answer}";
    }
}
