namespace Tools.Map;

public class Map<T>
{
    private Tile<T>[,] _map;

    private Map(Tile<T>[,] mapData)
    {
        _map = new Tile<T>[mapData.GetLength(0), mapData.GetLength(1)];

        Array.Copy(mapData, _map, mapData.Length);
    }

    public static Map<T> Load(string path)
    {
        var data = File
            .ReadAllLines(path)
            .Select((line, indexY) => line
                .Select((tileData, indexX) => new Tile<T>()
                {
                    X = indexX,
                    Y = indexY,
                    Value = (T)Convert.ChangeType(tileData, typeof(T)),
                }).ToArray())
            .ToArray();

        var mapData = new Tile<T>[data[0].Length, data.Length];

        for (var y = 0; y < data.Length; y++)
        {
            for (var x = 0; x < data[y].Length; x++)
            {
                mapData[x, y] = data[y][x];
            }
        }

        return new Map<T>(mapData);
    }

    public int Height => _map.GetLength(1);

    public int Width => _map.GetLength(0);

    public bool IsInBounds(int x, int y) => x >= 0 && y >= 0 && y < Height && x < Width;

    public Tile<T> this[int x, int y]
    {
        get => _map[x, y];
        set => _map[x, y] = value;
    }

    public bool TryGetValue(int x, int y, out Tile<T> result)
    {
        if (IsInBounds(x, y))
        {
            result = this[x, y];

            return true;
        }

        result = default!;

        return false;
    }

    public void Clear(T? clearValue = default)
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                _map[x, y] = new Tile<T>
                {
                    X = x,
                    Y = y,
                    Value = clearValue,
                };
            }
        }
    }

    public Map<T> CreateCopy()
    {
        var mapData = new Tile<T>[Width, Height];

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                mapData[x, y] = _map[x, y] with { };
            }
        }

        return new Map<T>(mapData);
    }
}
