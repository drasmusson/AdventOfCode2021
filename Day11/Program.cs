//https://adventofcode.com/2021/day/11
PartOne();
PartTwo();

void PartOne()
{
    var input = File.ReadAllLines("Day11/1.txt");
    var cave = new Dictionary<Coordinate, Octopus>();
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[y].Length; x++)
        {
            cave.Add(new Coordinate(x, y), new Octopus(new Coordinate(x, y), int.Parse(input[y][x].ToString())));
        }
    }

    var totalFlashes = 0;

    for (int i = 0; i < 100; i++)
    {
        foreach (var octopus in cave.Values)
        {
            octopus.IncreaseEnergyLevel();
        }
        var firstFlashedOctopusses = cave.Values.Where(o => o.Flashed).ToList();

        foreach (var flashedOctopus in firstFlashedOctopusses)
        {
            IncreaseNeighbours(cave, flashedOctopus);
        }

        totalFlashes += cave.Values.Where(o => o.Flashed).Count();
        foreach (var octopus in cave.Values)
        {
            if (octopus.Flashed)
            {
                octopus.Reset();
            }
        }
    }
    Console.WriteLine(totalFlashes);
}

void PartTwo()
{
    var input = File.ReadAllLines("Day11/1.txt");
    var cave = new Dictionary<Coordinate, Octopus>();
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[y].Length; x++)
        {
            cave.Add(new Coordinate(x, y), new Octopus(new Coordinate(x, y), int.Parse(input[y][x].ToString())));
        }
    }

    var synchronizedStep = 1;

    while(true)
    {
        foreach (var octopus in cave.Values)
        {
            octopus.IncreaseEnergyLevel();
        }
        var firstFlashedOctopusses = cave.Values.Where(o => o.Flashed).ToList();

        foreach (var flashedOctopus in firstFlashedOctopusses)
        {
            IncreaseNeighbours(cave, flashedOctopus);
        }

        if (cave.Values.Where(o => o.Flashed).Count() == cave.Values.Count())
        {
            break;
        }
        foreach (var octopus in cave.Values)
        {
            if (octopus.Flashed)
            {
                octopus.Reset();
            }
        }
        synchronizedStep++;
    }
    Console.WriteLine(synchronizedStep);
}

void IncreaseNeighbours(Dictionary<Coordinate, Octopus> cave, Octopus flashedOctopus)
{
    foreach (var neighbourCoordinate in flashedOctopus.NeighbouringCoordinates)
    {
        if (cave.TryGetValue(neighbourCoordinate, out var neighbourOctopus))
        {
            neighbourOctopus.IncreaseEnergyLevel();
            if (neighbourOctopus.Flashed && neighbourOctopus.EnergyLevel == 10)
            {
                IncreaseNeighbours(cave, neighbourOctopus);
            }
        }
    }
}

internal class Octopus
{
    public Coordinate Coordinate { get; set; }
    public int EnergyLevel { get; private set; }
    public bool Flashed { get; private set; }
    public IEnumerable<Coordinate> NeighbouringCoordinates { get; private set; }
    public Octopus(Coordinate coordinate, int initialEnergyLevel)
    {
        Coordinate = coordinate;
        NeighbouringCoordinates = GetNeighbouringCoordinates(coordinate);
        EnergyLevel = initialEnergyLevel;
    }

    public void Reset()
    {
        Flashed = false;
        EnergyLevel = 0;
    }

    public void IncreaseEnergyLevel()
    {
        EnergyLevel++;
        if (EnergyLevel == 10)
            Flashed = true;
    }

    private IEnumerable<Coordinate> GetNeighbouringCoordinates(Coordinate coordinate)
    {
        yield return coordinate with { X = coordinate.X - 1 };
        yield return coordinate with { X = coordinate.X + 1 };
        yield return coordinate with { Y = coordinate.Y - 1 };
        yield return coordinate with { Y = coordinate.Y + 1 };
        yield return coordinate with { X = coordinate.X + 1, Y = coordinate.Y + 1 };
        yield return coordinate with { X = coordinate.X + 1, Y = coordinate.Y - 1 };
        yield return coordinate with { X = coordinate.X - 1, Y = coordinate.Y - 1 };
        yield return coordinate with { X = coordinate.X - 1, Y = coordinate.Y + 1 };
    }
}

internal record Coordinate(int X, int Y);