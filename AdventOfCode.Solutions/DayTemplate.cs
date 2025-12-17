namespace AdventOfCode.Solutions._2025;

using System.Collections.Generic;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2099, 99)]
internal class DayX : IAdventOfCodeDay
{
    private IEnumerable<string> _input = [];

    public async Task Setup(string puzzleInput)
    {
        _input = puzzleInput.Split(Environment.NewLine);
    }

    public async Task<string> SolvePart1()
    {
        var answer = 0;

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = 0;

        return $"{answer}";
    }
}
