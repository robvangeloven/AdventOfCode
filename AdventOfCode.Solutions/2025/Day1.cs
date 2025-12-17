namespace AdventOfCode.Solutions._2025;

using System.Collections.Generic;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2025, 1)]
internal class Day1 : IAdventOfCodeDay
{
    private IEnumerable<string> _input = [];

    public async Task Setup(string puzzleInput)
    {
        _input = puzzleInput.Split(Environment.NewLine);
    }

    public async Task<string> SolvePart1()
    {
        var dialPosition = 50;
        var answer = 0;

        foreach (var dialInstruction in _input)
        {
            var instruction = dialInstruction[0];
            var number = int.Parse(dialInstruction[1..]);

            if (instruction == 'L')
            {
                dialPosition -= number;
            }
            else
            {
                dialPosition += number;
            }

            dialPosition = dialPosition % 100;

            if (dialPosition == 0)
            {
                answer++;
            }
        }

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var dialPosition = 50;
        int previousDialPosition;
        var answer = 0;

        foreach (var dialInstruction in _input)
        {
            previousDialPosition = dialPosition;

            var instruction = dialInstruction[0];
            var number = int.Parse(dialInstruction[1..]);

            // Count number of whole rotations:
            answer += Math.Abs(number / 100);

            // Remove whole rotations:
            number %= 100;

            if (instruction == 'L')
            {
                dialPosition -= number;
            }
            else
            {
                dialPosition += number;
            }

            switch (dialPosition)
            {
                case 0:
                    answer++;
                    break;

                case var x when x > 99:
                    // When previous rotation would land exactly on zero, this would double count:
                    if (previousDialPosition != 0)
                    {
                        answer++;
                    }

                    dialPosition %= 100;
                    break;

                case var x when x < 0:
                    // When previous rotation would land exactly on zero, this would double count:
                    if (previousDialPosition != 0)
                    {
                        answer++;
                    }

                    dialPosition += 100;
                    break;
            }
        }

        return $"{answer}";
    }
}
