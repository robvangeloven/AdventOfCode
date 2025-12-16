namespace AdventOfCode.Solutions._2024;

using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2024, 3)]
internal class Day3 : IAdventOfCodeDay
{
    private string _input = string.Empty;

    public async Task Setup(string puzzleInput)
    {
        _input = puzzleInput;
    }

    public async Task<string> SolvePart1()
    {
        var matches = Regex.Matches(_input, @"mul\((?<value>\d{1,3},\d{1,3})\)");

        var answer = matches.Select(match =>
        {
            var values = match.Groups["value"].Value.Split(',').Select(int.Parse).ToList();
            return values[0] * values[1];
        }).Sum();

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var matches = Regex.Matches(_input, @"\A(?<value>.*?)(?=don't)|(?<=do\(\))(?<value>.*?)(?=don't)|(?<=do\(\))(?<value>.*?)$", RegexOptions.Singleline);

        var answer = matches.SelectMany(x =>
        {
            return Regex
            .Matches(x.Groups["value"].Value, @"mul\((\d{1,3},\d{1,3})\)")
            .Select(match =>
            {
                var values = match.Groups[1].Value.Split(',').Select(int.Parse).ToList();
                return values[0] * values[1];
            });
        }).Sum();

        return $"{answer}";
    }
}
