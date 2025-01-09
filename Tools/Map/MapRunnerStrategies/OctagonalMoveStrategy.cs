namespace Tools.Map.MapRunnerStrategies;

using System.Collections.Generic;

public class OctagonalMoveStrategy<T> : IMapRunnerMoveStrategy<T>
{
    public IEnumerable<Tile<T>> GetNextPossibleMoves(int x, int y, Map<T> map)
    {
        var result = new List<Tile<T>>(8);

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
