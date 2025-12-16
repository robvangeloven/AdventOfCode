namespace AdventOfCode.Solutions._2024;

using System.Threading.Tasks;
using AdventOfCode.Attributes;
using AdventOfCode.Tools.Map;

[AdventOfCodeDay(2024, 10)]
internal class Day10 : IAdventOfCodeDay
{
    private Tile[][] _map = [];

    private Tile[][] CopyMap(Tile[][] map) => map.Select(y => y.Select(x => x with { Visited = false }).ToArray()).ToArray();

    private void DrawMap(Tile[][] map, (int x, int y) start, (int x, int y) current)
    {
        (var left, var top) = Console.GetCursorPosition();
        var currentColor = Console.ForegroundColor;

        Console.Write("   ");

        for (var i = 0; i < map.Length; i++)
        {
            Console.Write(i);
        }

        Console.WriteLine();

        for (var y = 0; y < map.Length; y++)
        {
            Console.Write($"{y}: ");

            for (var x = 0; x < map[y].Length; x++)
            {
                var tile = map[y][x];

                if (tile.Visited)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                if (current.x == x && current.y == y)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                if (start.x == x && start.y == y)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write(tile.Value < 0 ? "." : tile.Value);
                Console.ForegroundColor = currentColor;
            }

            Console.WriteLine();
        }

        Console.SetCursorPosition(left, top);
    }

    private int FindPath(int x, int y, int value, Tile[][] map, bool partTwo)
    {
        map[y][x].Visited = true;

        var result = 0;

        if (value == 9)
        {
            return 1;
        }

        var nextSteps = GetNextSteps(x, y, value, map, partTwo);

        foreach (var step in nextSteps)
        {
            result += FindPath(step.X, step.Y, step.Value, map, partTwo);
        }

        return result;
    }

    private bool CheckStep((int x, int y) currentValue, (int x, int y) targetValue, Tile[][] map, bool partTwo)
    {
        if (targetValue.y < 0 || targetValue.x < 0 || targetValue.y >= map.Length || targetValue.x >= map[targetValue.y].Length)
        {
            return false;
        }

        return (partTwo || !map[targetValue.y][targetValue.x].Visited) && map[targetValue.y][targetValue.x].Value - map[currentValue.y][currentValue.x].Value == 1;
    }

    private IEnumerable<Step> GetNextSteps(int x, int y, int startingValue, Tile[][] map, bool partTwo)
    {
        var result = new List<Step>();

        var yStart = y - 1 < 0 ? 0 : y - 1;
        var yEnd = y + 2 > map.Length ? map.Length : y + 2;

        var newX = x;
        var newY = y - 1;

        if (CheckStep((x, y), (newX, newY), map, partTwo))
        {
            result.Add(new Step(newX, newY, map[newY][newX].Value));
        }

        newY = y + 1;

        if (CheckStep((x, y), (newX, newY), map, partTwo))
        {
            result.Add(new Step(newX, newY, map[newY][newX].Value));
        }

        newY = y;
        newX = x - 1;

        if (CheckStep((x, y), (newX, newY), map, partTwo))
        {
            result.Add(new Step(newX, newY, map[newY][newX].Value));
        }

        newX = x + 1;

        if (CheckStep((x, y), (newX, newY), map, partTwo))
        {
            result.Add(new Step(newX, newY, map[newY][newX].Value));
        }

        return result;
    }

    public async Task Setup(string puzzleInput)
    {
        _map = puzzleInput
            .Split(Environment.NewLine)
            .Select(line => line.Select(x => new Tile(x == '.' ? -1 : int.Parse($"{x}"))).ToArray()).ToArray();
    }

    public async Task<string> SolvePart1()
    {
        var answer = 0;

        for (var y = 0; y < _map.Length; y++)
        {
            for (var x = 0; x < _map[y].Length; x++)
            {
                if (_map[y][x].Value == 0)
                {
                    var mapCopy = CopyMap(_map);
                    answer += FindPath(x, y, 0, mapCopy, false);
                }
            }
        }

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = 0;

        for (var y = 0; y < _map.Length; y++)
        {
            for (var x = 0; x < _map[y].Length; x++)
            {
                if (_map[y][x].Value == 0)
                {
                    var mapCopy = CopyMap(_map);
                    answer += FindPath(x, y, 0, mapCopy, true);
                }
            }
        }

        return $"{answer}";
    }

    private record Step(int X, int Y, int Value);

    private record Tile(int Value, bool Visited = false)
    {
        public bool Visited { get; set; } = Visited;
    };

}
