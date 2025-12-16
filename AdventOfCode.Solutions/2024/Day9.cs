namespace AdventOfCode.Solutions._2024;

using System.Threading.Tasks;
using AdventOfCode.Attributes;

[AdventOfCodeDay(2024, 9)]
internal class Day9 : IAdventOfCodeDay
{
    private string _input = string.Empty;

    public async Task Setup(string puzzleInput)
    {
        _input = puzzleInput;
    }

    public async Task<string> SolvePart1()
    {
        var emptyNodes = 0;
        var stack = new Stack<int>();

        var answer = _input
            .SelectMany((c, index) =>
            {
                var fileId = index / 2;

                return Enumerable
                   .Range(0, int.Parse($"{c}"))
                   .Select(_ =>
                   {
                       if (index % 2 == 0)
                       {
                           stack.Push(fileId);
                           return fileId;
                       }

                       emptyNodes++;
                       return (int?)null;
                   });

            })
            .ToArray()
            .Select(node => node ?? stack.Pop())
            .Take(..^emptyNodes)
            .Select((value, index) => index * (long)value)
            .Sum();

        return $"{answer}";
    }

    public async Task<string> SolvePart2()
    {
        var answer = 0L;

        var nodesToRemove = new List<Node>();
        var stack = new Stack<Node>();

        var nodes = _input
            .Chunk(2)
            .SelectMany((nodes, index) =>
            {
                var fileNode = new Node(index, int.Parse($"{nodes[0]}"), 0);

                stack.Push(fileNode);

                List<Node> result = nodes.Length > 1
                ? [fileNode, new Node((index + 1) * -1, 0, int.Parse($"{nodes[1]}"))]
                : [fileNode];

                return result;
            }).ToList();

        foreach (var node in stack)
        {
            var nodeIndex = nodes.FindIndex(x => x.NodeId == node.NodeId);

            var freeNodeIndex = nodes[..nodeIndex].FindIndex(x => x.FreeSpace >= node.BlocksUsed);

            if (freeNodeIndex < 0)
            {
                continue;
            }

            var freeNode = nodes.ElementAt(freeNodeIndex);

            freeNode.FreeSpace = freeNode.FreeSpace - node.BlocksUsed;

            nodes.Insert(freeNodeIndex, node with { });

            node.NodeId = (node.NodeId + 1) * -1;
            node.FreeSpace += node.BlocksUsed;
            node.BlocksUsed = 0;
        }

        var index = 0;

        answer = nodes
            .Where(node => node.FreeSpace > 0 || node.BlocksUsed > 0)
            .Select(node =>
            {
                if (node.NodeId < 0)
                {
                    index += node.FreeSpace;
                    return 0;
                }

                var result = Enumerable
                .Range(0, node.BlocksUsed)
                .Sum(i => index++ * (long)node.NodeId);

                index += node.FreeSpace > 0
                ? node.FreeSpace - 1
                : 0;

                return result;
            })
            .Sum();

        return $"{answer}";
    }

    private record Node(int NodeId, int BlocksUsed, int FreeSpace)
    {
        public int NodeId { get; set; } = NodeId;

        public int BlocksUsed { get; set; } = BlocksUsed;

        public int FreeSpace { get; set; } = FreeSpace;
    }
}
