PartOne();

void PartOne()
{
    var lines = File.ReadAllLines("Day09/1.txt").ToList();
    lines = lines.Select(l => "9" + l + "9").ToList();
    lines.Insert(0, new string('9', lines[0].Length));
    lines.Add(new string('9', lines[0].Length));

    var locations = new List<Location>();
    for (int y = 1; y < lines.Count - 1; y++)
    {
        for (int x = 1; x < lines[y].Length - 1; x++)
        {
            locations.Add(
                new Location(lines[y][x], new List<char> {
                lines[y][x-1],
                lines[y][x+1],
                lines[y - 1][x],
                lines[y + 1][x] })
                );
        }
    }

    var result = locations.Where(l => l.IsLowPoint)
        .Select(l => l.RiskLevel)
        .Sum();

    Console.WriteLine(result);
}

internal class Location
{
    public int Depth { get; private set; }
    public List<int> Neighbours { get; private set; }
    public bool IsLowPoint => Neighbours.All(n => n > Depth);
    public int RiskLevel => Depth + 1;
    public Location(char depth, List<char> neighbours)
    {
        Depth = int.Parse(depth.ToString());
        Neighbours = neighbours.Select(n => int.Parse(n.ToString())).ToList();
    }
}