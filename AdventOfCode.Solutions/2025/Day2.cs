namespace AdventOfCode.Solutions._2025;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2025, 2)]
internal class Day2 : IAdventOfCodeDay
{
    private IEnumerable<string> _input = [];

    public async Task Setup(string puzzleInput)
    {
        _input = puzzleInput.Split(',');
    }

    public async Task<string> SolvePart1()
    {
        var answer = _input.Sum(x =>
        {
            long sum = 0;
            var productIdRanges = x.Split('-').Select(long.Parse).ToList();

            var productIdRangeStart = productIdRanges[0];
            var productIdRangeEnd = productIdRanges[1];

            for (var productId = productIdRangeStart; productId <= productIdRangeEnd; productId++)
            {
                var productIdstring = $"{productId}";

                if (productIdstring.Length % 2 == 0)
                {
                    var matchLength = productIdstring.Length / 2;

                    if (Regex.IsMatch($"{productId}", $"([0-9]{{{matchLength},{matchLength}}})(\\1{{1,}})"))
                    {
                        sum += productId;
                    }
                }
            }

            return sum;
        });

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = _input.Sum(x =>
        {
            long sum = 0;
            var productIdRanges = x.Split('-').Select(long.Parse).ToList();

            var productIdRangeStart = productIdRanges[0];
            var productIdRangeEnd = productIdRanges[1];

            for (var productId = productIdRangeStart; productId <= productIdRangeEnd; productId++)
            {
                var productIdstring = $"{productId}";

                for (var i = productIdstring.Length / 2; i >= 1; i--)
                {
                    if (Regex.Match($"{productId}", $"([0-9]{{{i},{i}}})(\\1{{1,}})").Value == productIdstring)
                    {
                        sum += productId;
                        break;
                    }
                }
            }

            return sum;
        });

        return $"{answer}";
    }
}
