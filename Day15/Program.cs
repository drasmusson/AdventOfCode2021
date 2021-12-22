// https://adventofcode.com/2021/day/15

PartOne();
PartTwo();

void PartOne()
{
    var lines = File.ReadAllLines("Day15/1.txt").ToArray();

    var cave = new Dictionary<Coord, Node>();
    for (int y = 0; y < lines.Length; y++)
    {
        for (var x = 0; x < lines[y].Length; x++)
        {
            cave.Add(new Coord(x, y), new Node(int.Parse(lines[y][x].ToString()), long.MaxValue));
        }
    }
    cave[new Coord(0, 0)].RiskSum = 0L;

    TraverseAndAddRiskSums(cave);
    var endCoord = new Coord(lines.Length - 1, lines[0].Length - 1);
    var result = cave[endCoord].RiskSum;
    Console.WriteLine(result);
}

void PartTwo()
{
    var lines = File.ReadAllLines("Day15/1.txt").ToArray();
    var maxWidth = lines.Length;
    var cave = new Dictionary<Coord, Node>();
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            for (int y = 0; y < maxWidth; y++)
            {
                for (var x = 0; x < maxWidth; x++)
                {
                    var riskInput = int.Parse(lines[y][x].ToString());

                    var risk = riskInput + i + j;

                    risk = risk > 9 ? risk % 9 : risk;

                    var xValue = x + maxWidth * j;
                    var yValue = y + maxWidth * i;
                    cave.Add(new Coord(xValue, yValue), new Node(risk, long.MaxValue));
                }
            }
        }
    }

    cave[new Coord(0, 0)].RiskSum = 0L;

    TraverseAndAddRiskSums(cave);
    var width = int.Parse(Math.Sqrt(cave.Count).ToString()) - 1;
    var endCoord = new Coord(width, width);
    var result = cave[endCoord].RiskSum;
    Console.WriteLine(result);
}

void TraverseAndAddRiskSums(Dictionary<Coord, Node> cave)
{
    var queue = new Queue<Coord>();
    queue.Enqueue(new Coord(0, 0));
    do
    {
        var currentCoord = queue.Dequeue();
        var currentNode = cave[currentCoord];
        foreach (var neighbourCoord in GetNeighbouringCoords(currentCoord))
        {
            if (cave.TryGetValue(neighbourCoord, out var neighbourNode))
            {
                if (currentNode.RiskSum + neighbourNode.Risk < neighbourNode.RiskSum)
                {
                    neighbourNode.RiskSum = currentNode.RiskSum + neighbourNode.Risk;
                    queue.Enqueue(neighbourCoord);
                }
            }
        }
    } while (queue.Any());
}

IEnumerable<Coord> GetNeighbouringCoords(Coord coord)
{
    yield return coord with { X = coord.X - 1 };
    yield return coord with { X = coord.X + 1 };
    yield return coord with { Y = coord.Y - 1 };
    yield return coord with { Y = coord.Y + 1 };
}

internal record Coord(int X, int Y);

internal class Node
{
    public Node(int value, long sum)
    {
        Risk = value;
        RiskSum = sum;
    }

    public int Risk { get; set; }
    public long RiskSum { get; set; }
}
