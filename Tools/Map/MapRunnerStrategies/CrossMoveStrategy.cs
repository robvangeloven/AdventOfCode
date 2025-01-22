namespace Tools.Map.MapRunnerStrategies;

using System.Collections.Generic;

public class CrossMoveStrategy<T> : IMapRunnerMoveStrategy<T>
{
    public IEnumerable<Tile<T>> GetNextPossibleMoves(Tile<T> startTile, Map<T> map)
    {
        var result = new List<Tile<T>>(4);
        var x = startTile.X;
        var y = startTile.Y;

        if (map.TryGetValue(x - 1, y, out var tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x + 1, y, out tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x, y - 1, out tile))
        {
            result.Add(tile);
        }

        if (map.TryGetValue(x, y + 1, out tile))
        {
            result.Add(tile);
        }

        return result;
    }
}
