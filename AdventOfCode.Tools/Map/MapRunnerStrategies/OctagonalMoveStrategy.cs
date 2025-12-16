namespace AdventOfCode.Tools.Map.MapRunnerStrategies;

using System.Collections.Generic;
using AdventOfCode.Tools.Map;

public class OctagonalMoveStrategy<T> : IMapRunnerMoveStrategy<T>
{
    public IEnumerable<Tile<T>> GetNextPossibleMoves(Tile<T> startTile, Map<T> map)
    {
        var result = new List<Tile<T>>(8);
        var x = startTile.X;
        var y = startTile.Y;

        if (map.TryGetValue(x - 1, y - 1, out var tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x, y - 1, out tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x + 1, y - 1, out tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x - 1, y, out tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x + 1, y, out tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x - 1, y + 1, out tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x, y + 1, out tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x + 1, y + 1, out tile))
        {
            result.Add(tile);
        }

        return result;
    }
}
