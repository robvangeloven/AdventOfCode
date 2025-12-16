namespace AdventOfCode.Tools.Map.MapRunner;

using System.Collections.Generic;
using AdventOfCode.Tools.Map;
using Tools.Map.MapRunnerStrategies;

public class RegionMapper<T>
{
    //public Map<T> MapRegion(Map<T> map, Tile<T> startTile)
    //{
    //    var result = map.CreateCopy();
    //    result.Clear();

    //    var moveStrategey = new OctagonalMoveStrategy<T>();

    //    Queue<Tile<T>> queue = new([startTile]);

    //    while (queue.TryDequeue(out var tile))
    //    {
    //        if (!tile.Visited)
    //        {
    //            result[tile.X, tile.Y] = tile;

    //            tile.Visit();

    //            foreach (var childTile in moveStrategey
    //                .GetNextPossibleMoves(tile, map)
    //                .Where(t => !t.Visited && t.Value?.Equals(startTile.Value) is true))
    //            {
    //                queue.Enqueue(childTile);
    //            }
    //        }
    //    }

    //    return result;
    //}

    public Region MapRegion(Map<T> map, Tile<T> startTile)
    {
        var area = 0;
        var perimiter = 0;

        var moveStrategey = new OctagonalMoveStrategy<T>();
        var perimiterScanStrategy = new CrossMoveStrategy<T>();

        Queue<Tile<T>> queue = new([startTile]);

        while (queue.TryDequeue(out var tile))
        {
            if (!tile.Visited)
            {
                area++;

                perimiter += 4 - perimiterScanStrategy
                        .GetNextPossibleMoves(tile, map)
                        .Count(tile => tile.Value?.Equals(startTile.Value) is true);

                tile.Visit();

                foreach (var childTile in moveStrategey
                    .GetNextPossibleMoves(tile, map)
                    .Where(t => !t.Visited && t.Value?.Equals(startTile.Value) is true))
                {
                    queue.Enqueue(childTile);
                }
            }
        }

        return new()
        {
            Area = area,
            Perimiter = perimiter,
        };
    }
}
