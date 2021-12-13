//https://adventofcode.com/2021/day/12
PartOne();
Console.WriteLine("Hello, World!");

void PartOne()
{
    var input = File.ReadAllLines("Day12/1.txt");
    var caves = input.SelectMany(x => x.Split('-')).ToHashSet().Select(i => new Cave(i)).ToList();

    foreach (var connection in input.Select(x => x.Split('-')))
    {
        caves.First(c => c.Name == connection[0]).AddConnectedCave(caves.First(x => x.Name == connection[1]));
        caves.First(c => c.Name == connection[1]).AddConnectedCave(caves.First(x => x.Name == connection[0]));
    }
    var start = caves.First(c => c.Start);
    var result = 0;
    var t = start.Traverse(result);
    Console.WriteLine(t);
}

internal class Cave
{
    public string Name { get; private set; }
    public bool IsSmallCave { get; private set; }
    public bool Start { get; private set; }
    public bool End { get; private set; }
    public List<Cave> ConnectedCaves { get; private set; }
    public bool CantBeTraversed => (Start && TimesTraversed > 0) || (IsSmallCave && TimesTraversed > 0);
    private int TimesTraversed;

    public Cave(string name)
    {
        Name = name;

        switch (name)
        {
            case "start":
                Start = true;
                break;
            case "end":
                End = true;
                break;
            default:
                break;
        }
        if (name.Length == 1 && char.IsLower(name[0]))
            IsSmallCave = true;

        ConnectedCaves = new List<Cave>();
    }

    public void AddConnectedCave(Cave cave)
    {
        ConnectedCaves.Add(cave);
    }

    public int Traverse(int numberOfRoutesToEnd)
    {
        if (!End)
        {
            
            TimesTraversed++;
            if (ConnectedCaves.All(cc => cc.CantBeTraversed))
            {
                return 0;
            }
            foreach (var connectedCave in ConnectedCaves)
            {
                if (!connectedCave.CantBeTraversed)
                {
                    numberOfRoutesToEnd += connectedCave.Traverse(numberOfRoutesToEnd);
                }
            }
        }
        numberOfRoutesToEnd++;
        return numberOfRoutesToEnd;
    }
}