namespace AdventOfCode.Solutions._2024;

using System.Collections.Concurrent;
using System.Threading.Tasks;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2024, 11)]
internal class Day11 : IAdventOfCodeDay
{
    private ConcurrentDictionary<(long number, int count), long> cache = [];
    private List<long> input = [];

    private long Blink(long number, int times)
    {
        if (times == 0)
        {
            return 1;
        }

        var cacheKey = (number, times);

        if (cache.TryGetValue(cacheKey, out var cachedValue))
        {
            return cachedValue;
        }

        var result = 0L;

        if (number == 0)
        {
            result = Blink(1, times - 1);
        }
        else
        {
            var digit = number.ToString();

            if (digit.Length % 2 == 0)
            {
                var half = digit.Length / 2;
                result = Blink(long.Parse(digit[0..half]), times - 1) + Blink(long.Parse(digit[half..]), times - 1);
            }
            else
            {
                result = Blink(number * 2024, times - 1);
            }
        }

        cache.AddOrUpdate(cacheKey, result, (_, value) => value);

        return result;
    }

    public async Task Setup(string puzzleInput)
    {
        input = puzzleInput
            .Split(' ')
            .Select(long.Parse)
            .ToList();
    }

    public async Task<string> SolvePart1()
    {
        var answer = input
        .AsParallel()
        .Select(x => Blink(x, 25))
        .Sum();

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = input
        .AsParallel()
        .Select(x => Blink(x, 75))
        .Sum();

        return $"{answer}";
    }
}
