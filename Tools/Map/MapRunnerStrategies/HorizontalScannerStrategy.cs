namespace Tools.Map.MapRunnerStrategies;

using System.Collections.Generic;

public class HorizontalScannerStrategy<T> : IMapRunnerMoveStrategy<T>
{
    public IEnumerable<Tile<T>> GetNextPossibleMoves(Tile<T> startTile, Map<T> map)
    {
        return
            map.TryGetValue(startTile.X + 1, startTile.Y, out var nextTile) ||
            map.TryGetValue(0, startTile.Y + 1, out nextTile)
            ? [nextTile]
            : [];
    }
}
