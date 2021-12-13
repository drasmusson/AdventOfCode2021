//https://adventofcode.com/2021/day/9
PartOne();
PartTwo();

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

void PartTwo()
{
    var lines = File.ReadAllLines("Day09/1.txt").ToList();
    lines = lines.Select(l => "9" + l + "9").ToList();
    lines.Insert(0, new string('9', lines[0].Length));
    lines.Add(new string('9', lines[0].Length));

    var locations = new List<LocationV2>();
    for (int y = 1; y < lines.Count - 1; y++)
    {
        for (int x = 1; x < lines[y].Length - 1; x++)
        {
            locations.Add(
                new LocationV2(lines[y][x], new Coord(x, y)));
        }
    }

    foreach (var location in locations)
    {
        location.AddNeighbours(locations);
    }

    var lowPoints = locations.Where(l => l.IsLowPoint).ToList();
    var basins = new List<int>();
    foreach (var lowPoint in lowPoints)
    {
        var neighbours = new List<LocationV2>();
        lowPoint.GetParents(neighbours);
        basins.Add(neighbours.Count);
    }
    basins.Sort();
    basins.Reverse();
    var largestBasins = basins.Take(3);
    var result = largestBasins.Aggregate((a, x) => a * x);
    Console.WriteLine(result);
}

internal class LocationV2
{
    public List<LocationV2> Neighbours { get; private set; }
    public bool IsLowPoint => Neighbours.All(n => n.Depth > Depth);
    public int RiskLevel => Depth + 1;
    public int Depth { get; private set; }
    public Coord Coord { get; private set; }

    public LocationV2(char depth, Coord coord)
    {
        Depth = int.Parse(depth.ToString());
        Coord = coord;
        Neighbours = new List<LocationV2>();
    }

    public List<LocationV2> GetParents(List<LocationV2> parents)
    {
        if (parents.Contains(this))
        {
            return parents;
        }
        parents.Add(this);
        var higherNeighbours = Neighbours.Where(n => n.Depth > Depth && n.Depth != 9).ToList();
        if (higherNeighbours.Count == 0)
        {
            return parents;
        }
        var tree = higherNeighbours.SelectMany(n => n.GetParents(parents)).ToList();
        parents = tree.Distinct().ToList();
        return parents;
    }

    public void AddNeighbours(List<LocationV2> locations)
    {
        Neighbours = locations.Where(l =>
            l.Coord.IsNextToCoord(Coord)).ToList();
    }
}

internal record Coord (int X, int Y)
{
    public bool IsNextToCoord(Coord coord) => 
        (coord.X - 1 == X && coord.Y == Y) ||
        (coord.X + 1 == X && coord.Y == Y) ||
        (coord.X == X && coord.Y - 1 == Y) ||
        (coord.X == X && coord.Y + 1 == Y);
}

internal class Location
{
    public List<int> Neighbours { get; private set; }
    public bool IsLowPoint => Neighbours.All(n => n > Depth);
    public int RiskLevel => Depth + 1;
    private int Depth;
    public Location(char depth, List<char> neighbours)
    {
        Depth = int.Parse(depth.ToString());
        Neighbours = neighbours.Select(n => int.Parse(n.ToString())).ToList();
    }
}