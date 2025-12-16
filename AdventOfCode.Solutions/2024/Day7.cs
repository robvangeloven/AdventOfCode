namespace AdventOfCode.Solutions._2024;

using System.Threading.Tasks;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2024, 7)]
internal class Day7 : IAdventOfCodeDay
{
    private string _input = string.Empty;
    private IEnumerable<SumRecord> sums = [];

    private bool ValidateSum(long sum, long[] operands, bool partTwo)
    {
        if (operands.Length == 1)
        {
            return sum == operands[0];
        }

        return ValidateSum(sum, [operands[0] + operands[1], .. operands[2..]], partTwo) ||
            ValidateSum(sum, [operands[0] * operands[1], .. operands[2..]], partTwo) ||
            (partTwo && ValidateSum(sum, [long.Parse($"{operands[0]}{operands[1]}"), .. operands[2..]], partTwo));
    }

    public async Task Setup(string puzzleInput)
    {
        var input = puzzleInput.Split(Environment.NewLine);

        sums = input.Select(x =>
        {
            var values = x.Split(": ");
            var sum = values[0];
            var operands = values[1].Split(' ');

            return new SumRecord
            {
                Sum = long.Parse(sum),
                Operands = operands.Select(long.Parse).ToArray(),
            };
        });
    }

    public async Task<string> SolvePart1()
    {
        long answer = 0;

        answer = sums
            .AsParallel()
            .Where(x => ValidateSum(x.Sum, x.Operands, false))
            .Sum(x => x.Sum);

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        long answer = 0;

        answer = sums
            .AsParallel()
            .Where(x => ValidateSum(x.Sum, x.Operands, true))
            .Sum(x => x.Sum);

        return $"{answer}";
    }

    private record SumRecord
    {
        public required long Sum { get; init; }

        public required long[] Operands { get; init; }
    }
}
