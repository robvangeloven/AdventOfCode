namespace AdventOfCode.Solutions._2024;

using System.Threading.Tasks;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2024, 8)]
internal class Day8 : IAdventOfCodeDay
{
    private char[][] _map = [];
    private Dictionary<char, IList<Point>> _antennas = [];
    private HashSet<Point> _pointSet = [];
    private const char _emptySpace = '.';
    private const char _marker = '#';

    private void PrintMap(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[y].Length; x++)
            {
                var consoleColor = Console.ForegroundColor;

                if (map[y][x] != _emptySpace)
                {
                    if (map[y][x] == _marker)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }

                Console.Write(map[y][x]);

                Console.ForegroundColor = consoleColor;
            }

            Console.WriteLine();
        }
    }

    private bool MarkAntinodeOnMap(Point point, char[][] map)
    {
        if (point.Y >= 0 && point.Y < map.Length &&
            point.X >= 0 && point.X < map[point.Y].Length)
        {
            _pointSet.Add(point);
            map[point.Y][point.X] = _marker;
            return true;
        }

        return false;
    }

    private int CountAntinodesPartTwo(char[][] map)
    {
        return map.Sum(x => x.Count(y =>
        {
            if (y == _emptySpace)
            {
                return false;
            }

            if (_antennas.TryGetValue(y, out var points))
            {
                if (points.Count <= 1)
                {
                    return false;
                }
            }

            return true;
        }));
    }

    private void MapAntinodes(Point point, Point[] antennas, char[][] map)
    {
        foreach (var antenna in antennas)
        {
            var antiNode = antenna + (antenna - point);
            MarkAntinodeOnMap(antiNode, map);
        }
    }

    private void MapAntinodesPartTwo(Point point, Point[] antennas, char[][] map)
    {
        foreach (var antenna in antennas)
        {
            var distance = antenna - point;

            var antiNode = antenna + distance;
            var marked = MarkAntinodeOnMap(antiNode, map);

            while (marked)
            {
                antiNode = antiNode + distance;
                marked = MarkAntinodeOnMap(antiNode, map);
            }
        }
    }

    public async Task Setup(string puzzleInput)
    {
        var input = puzzleInput.Split(Environment.NewLine);
        _map = input.Select(line => line.ToArray()).ToArray();

        for (var y = 0; y < _map.Length; y++)
        {
            for (var x = 0; x < _map[y].Length; x++)
            {
                var terrain = _map[y][x];
                if (terrain != _emptySpace)
                {
                    if (!_antennas.ContainsKey(terrain))
                    {
                        _antennas[terrain] = [];
                    }

                    _antennas[terrain].Add(new Point(x, y));
                }
            }
        }
    }

    public async Task<string> SolvePart1()
    {
        var answer = 0;

        foreach (var antenna in _antennas)
        {
            foreach (var point in antenna.Value)
            {
                MapAntinodes(point, antenna.Value.Where(x => x != point).ToArray(), _map);
            }
        }

        answer = _pointSet.Count;

        return $"{answer}";
    }

    private char[][] CopyMap(char[][] map)
    {
        char[][] mapCopy = new char[map.Length][];

        for (var y = 0; y < map.Length; y++)
        {
            mapCopy[y] = new char[map[y].Length];

            for (var x = 0; x < map.Length; x++)
            {
                mapCopy[y][x] = map[y][x];
            }
        }

        return mapCopy;
    }

    public async Task<string> SolvePart2()
    {
        var answer = 0;

        foreach (var antenna in _antennas)
        {
            var mapCopy = CopyMap(_map);

            foreach (var point in antenna.Value)
            {
                MapAntinodesPartTwo(point, antenna.Value.Where(x => x != point).ToArray(), _map);
            }
        }

        answer = _pointSet.Count + _antennas.Where(x => x.Value.Count > 1).Sum(x => x.Value.Count - 1);

        var bla = CountAntinodesPartTwo(_map);

        return $"{answer}";
    }

    private record Point(int X, int Y)
    {
        public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

        public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    }
}
