namespace Tools.Map.MapRunner;

using System;
using System.Collections.Generic;
using Tools.Map.MapRunnerStrategies;

public class PerimiterRunner<T> : MapRunner<T>
{
    public PerimiterRunner(Tile<T> startingTile, Map<T> map) : base(startingTile, new HorizontalScannerStrategy<T>(), map)
    {
    }

    protected override Tile<T>? DecideNextMove(IEnumerable<Tile<T>> possibleMoves) => possibleMoves.FirstOrDefault();

    protected override void Visit(Tile<T> tile) => throw new NotImplementedException();
}
