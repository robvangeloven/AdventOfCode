namespace AdventOfCode.Tools.Map.MapRunner;

using System.Collections.Generic;
using AdventOfCode.Tools.Map;
using Tools.Map.MapRunnerStrategies;

public class RegionRunner<T> : MapRunner<T>
{
    private readonly List<Region> _regions;
    private readonly RegionMapper<T> _regionMapper;
    private readonly Map<T> _map;

    public RegionRunner(
        Tile<T> startingPosition,
        Map<T> map) : base(startingPosition, new HorizontalScannerStrategy<T>(), map)
    {
        _regions = [];
        _regionMapper = new();
        _map = map ?? throw new ArgumentNullException(nameof(map));
    }

    protected override Tile<T>? DecideNextMove(IEnumerable<Tile<T>> possibleMoves)
    {
        var tile = possibleMoves.FirstOrDefault();

        return tile;
    }

    protected override void Visit(Tile<T> tile)
    {
        if (tile.Visited == false)
        {
            _regions.Add(_regionMapper.MapRegion(_map, tile));
        }
    }

    public IEnumerable<Region> Regions => _regions;
}
