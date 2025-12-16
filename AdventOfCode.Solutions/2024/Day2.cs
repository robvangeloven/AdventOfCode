namespace AdventOfCode.Solutions._2024;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2024, 2)]
internal class Day2 : IAdventOfCodeDay
{
    private List<List<int>> _reports = [];

    private static bool ScanReport(IList<int> report)
    {
        var deltas = report.Zip(report.Skip(1), (a, b) => b - a);

        return deltas.All(delta => delta is >= 1 and <= 3) ||
            deltas.All(delta => delta is <= -1 and >= -3);
    }

    public async Task Setup(string puzzleInput)
    {
        _reports = [.. puzzleInput.Split(Environment.NewLine).Select(line => line.Split(' ').Select(int.Parse).ToList())];

    }
    public async Task<string> SolvePart1()
    {
        var answer = _reports.Where(ScanReport).Count();

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = _reports
            .Where(report =>
            {
                var safeReport = ScanReport(report);

                if (safeReport)
                {
                    return true;
                }

                for (int i = 0; i < report.Count; i++)
                {
                    var repo = new List<int>(report);
                    repo.RemoveAt(i);

                    if (ScanReport(repo))
                    {
                        return true;
                    }
                }

                return false;
            });

        return $"{answer.Count()}";
    }
}
