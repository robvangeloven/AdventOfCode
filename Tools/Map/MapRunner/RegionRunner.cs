namespace Tools.Map.MapRunner;

using System.Collections.Generic;

public record RegionRunner<T> : MapRunner<T>
{
    private List<Map<T>> _regions = [];

    public RegionRunner(
        (int x, int y) startingPosition,
        IMapRunnerMoveStrategy<T> mapRunnerMoveStrategy,
        Map<T> map) : base(startingPosition, mapRunnerMoveStrategy, map)
    {
    }

    public override Tile<T>? DecideNextMove(IEnumerable<Tile<T>> possibleMoves)
    {
        foreach (var tile in possibleMoves.Where(x => !x.Visited))
        {

        }

        return possibleMoves.First();
    }
}
