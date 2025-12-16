namespace AdventOfCode.Solutions._2024;

using System.Threading.Tasks;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2024, 5)]
internal class Day5 : IAdventOfCodeDay
{
    private List<List<int>> _manuals = [];
    private Dictionary<int, List<int>> _rules = [];

    private bool IsValidPage(IList<int> pages)
    {
        return _rules.All(ruleSet =>
        {
            var ruleIndex = pages.IndexOf(ruleSet.Key);

            if (ruleIndex < 0)
            {
                return true;
            }

            return ruleSet.Value.All(rule =>
            {
                var pageIndex = pages.IndexOf(rule);

                return pageIndex < 0
                ? true
                : ruleIndex < pageIndex;
            });
        });
    }

    public async Task Setup(string puzzleInput)
    {
        var input = puzzleInput.Split(Environment.NewLine);
        
        var instructionProcessing = true;

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                instructionProcessing = false;
                continue;
            }

            if (instructionProcessing)
            {
                var values = line.Split('|').Select(int.Parse).ToArray();

                if (!_rules.ContainsKey(values[0]))
                {
                    _rules[values[0]] = [];
                }

                _rules[values[0]].Add(values[1]);
            }
            else
            {
                _manuals.Add([.. line.Split(',').Select(int.Parse)]);
            }
        }
    }

    public async Task<string> SolvePart1()
    {
        var answer = 0;

        answer = _manuals.Sum(manual => IsValidPage(manual) ? manual[(manual.Count - 1) / 2] : 0);

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = 0;

        answer = _manuals.Sum(manual =>
        {
            if (IsValidPage(manual))
            {
                return 0;
            }

            manual.Sort((pageA, pageB) =>
            {
                if (!_rules.TryGetValue(pageA, out var pageARules))
                {
                    return 1;
                }

                if (!_rules.TryGetValue(pageB, out var pageBRules))
                {
                    return -1;
                }

                return pageARules.Contains(pageB)
                ? -1
                : 1;
            });

            return manual[(manual.Count - 1) / 2];
        });

        return $"{answer}";
    }
}
