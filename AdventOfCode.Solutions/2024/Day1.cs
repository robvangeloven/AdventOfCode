namespace AdventOfCode.Solutions._2024;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2024, 1)]
internal class Day1 : IAdventOfCodeDay
{
    private readonly List<int> _listOne = [];
    private readonly List<int> _listTwo = [];

    public async Task Setup(string puzzleInput)
    {
        foreach (var line in puzzleInput.Split(Environment.NewLine))
        {
            var values = line.Split("  ");

            _listOne.Add(int.Parse(values[0]));
            _listTwo.Add(int.Parse(values[1]));
        }

        _listOne.Sort();
        _listTwo.Sort();
    }

    public async Task<string> SolvePart1()
    {
        var answer = 0;

        for (var i = 0; i < _listOne.Count; i++)
        {
            answer += Math.Abs(_listOne[i] - _listTwo[i]);
        }

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = 0;

        foreach (var item in _listOne)
        {
            var count = _listTwo.FindAll(x => x == item).Count;

            answer += item * count;
        }

        return $"{answer}";
    }
}
