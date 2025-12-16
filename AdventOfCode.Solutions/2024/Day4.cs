namespace AdventOfCode.Solutions._2024;

using System.Threading.Tasks;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2024, 4)]
internal class Day4 : IAdventOfCodeDay
{
    private char[][] _matrix = [];

    public async Task Setup(string puzzleInput)
    {
        _matrix = puzzleInput.Split(Environment.NewLine).Select(line => line.ToArray()).ToArray();
    }

    public async Task<string> SolvePart1()
    {
        var answer = 0;

        var width = _matrix[0].Length;
        var height = _matrix.Length;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (_matrix[y][x] != 'X')
                {
                    continue;
                }

                if (x <= width - 4)
                {
                    if (_matrix[y][x + 1] == 'M' &&
                        _matrix[y][x + 2] == 'A' &&
                        _matrix[y][x + 3] == 'S')
                    {
                        answer++;
                    }

                    if (y <= height - 4)
                    {
                        if (_matrix[y + 1][x + 1] == 'M' &&
                            _matrix[y + 2][x + 2] == 'A' &&
                            _matrix[y + 3][x + 3] == 'S')
                        {
                            answer++;
                        }
                    }

                    if (y >= 3)
                    {
                        if (_matrix[y - 1][x + 1] == 'M' &&
                            _matrix[y - 2][x + 2] == 'A' &&
                            _matrix[y - 3][x + 3] == 'S')
                        {
                            answer++;
                        }
                    }
                }

                if (x >= 3)
                {
                    if (_matrix[y][x - 1] == 'M' &&
                        _matrix[y][x - 2] == 'A' &&
                        _matrix[y][x - 3] == 'S')
                    {
                        answer++;
                    }

                    if (y <= height - 4)
                    {
                        if (_matrix[y + 1][x - 1] == 'M' &&
                            _matrix[y + 2][x - 2] == 'A' &&
                            _matrix[y + 3][x - 3] == 'S')
                        {
                            answer++;
                        }
                    }

                    if (y >= 3)
                    {
                        if (_matrix[y - 1][x - 1] == 'M' &&
                            _matrix[y - 2][x - 2] == 'A' &&
                            _matrix[y - 3][x - 3] == 'S')
                        {
                            answer++;
                        }
                    }
                }

                if (y <= height - 4)
                {
                    if (_matrix[y + 1][x] == 'M' &&
                        _matrix[y + 2][x] == 'A' &&
                        _matrix[y + 3][x] == 'S')
                    {
                        answer++;
                    }
                }

                if (y >= 3)
                {
                    if (_matrix[y - 1][x] == 'M' &&
                        _matrix[y - 2][x] == 'A' &&
                        _matrix[y - 3][x] == 'S')
                    {
                        answer++;
                    }
                }
            }
        }

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = 0;

        var width = _matrix[0].Length;
        var height = _matrix.Length;

        for (var y = 1; y < height - 1; y++)
        {
            for (var x = 1; x < width - 1; x++)
            {
                if (_matrix[y][x] is not 'A')
                {
                    continue;
                }

                if (((_matrix[y - 1][x - 1] is 'M' && _matrix[y + 1][x + 1] is 'S') ||
                    (_matrix[y - 1][x - 1] is 'S' && _matrix[y + 1][x + 1] is 'M'))
                    &&
                    ((_matrix[y + 1][x - 1] is 'M' && _matrix[y - 1][x + 1] is 'S') ||
                    (_matrix[y + 1][x - 1] is 'S' && _matrix[y - 1][x + 1] is 'M')))
                {
                    answer++;
                    continue;
                }
            }
        }

        return $"{answer}";
    }
}
