namespace AdventOfCode.Solutions;

public interface IAdventOfCodeDay
{
    Task Setup(string input);

    Task<string> SolvePart1();

    Task<string> SolvePart2();
}
