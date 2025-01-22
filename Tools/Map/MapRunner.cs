namespace Tools.Map;

public abstract class MapRunner<T>
{
    private readonly Map<T> _map;
    private Tile<T> _currentPosition;
    private readonly IMapRunnerMoveStrategy<T> _mapRunnerMoveStrategy;

    public MapRunner(
        Tile<T> startingPosition,
        IMapRunnerMoveStrategy<T> mapRunnerMoveStrategy,
        Map<T> map)
    {
        _currentPosition = startingPosition ?? throw new ArgumentNullException(nameof(startingPosition));
        _mapRunnerMoveStrategy = mapRunnerMoveStrategy ?? throw new ArgumentNullException(nameof(mapRunnerMoveStrategy));
        _map = map ?? throw new ArgumentNullException(nameof(map));
    }

    public bool MoveNext()
    {
        var possibleMoves = _mapRunnerMoveStrategy.GetNextPossibleMoves(_currentPosition, _map);

        var nextMove = DecideNextMove(possibleMoves);

        if (nextMove is not null)
        {
            _currentPosition = nextMove;

            Visit(_currentPosition);
        }

        return nextMove != null;
    }

    public void Run()
    {
        Visit(_currentPosition);

        while (MoveNext())
        {
        }
    }

    protected abstract Tile<T>? DecideNextMove(IEnumerable<Tile<T>> possibleMoves);

    protected abstract void Visit(Tile<T> tile);
}
