namespace AdventOfCode.Tools.Map.MapRunnerStrategies;

using AdventOfCode.Tools.Map;

public interface IMapRunnerMoveStrategy<T>
{
    IEnumerable<Tile<T>> GetNextPossibleMoves(Tile<T> startTile, Map<T> map);
}
