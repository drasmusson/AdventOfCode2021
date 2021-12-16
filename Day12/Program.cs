//https://adventofcode.com/2021/day/12

PartOne();
PartTwo();

void PartOne()
{
    var input = File.ReadAllLines("Day12/1.txt").ToList();
    var caveMap = new Dictionary<string, List<Cave>>();
    input.SelectMany(x => x.Split("-"))
        .Distinct().ToList().
        ForEach(caveName =>
        {
            var neighbouringCaves = input.Where(x => x.Split('-')[0] == caveName || x.Split('-')[1] == caveName).SelectMany(x => x.Split('-')).Where(x => x != caveName).Select(x => CreateCave(x)).ToList();
            caveMap.Add(caveName, neighbouringCaves);
        });
    
    var log = new List<string>();
    Traverse(new Start(), new List<Cave>());
    Console.WriteLine(log.Distinct().ToList().Count);

    void Traverse(Cave current, List<Cave> visited)
    {
        visited.Add(current);
        if (current is End)
        {
            log.Add(string.Join(",", visited.Select(x => x.Name)));
            visited = new();
            return;
        }

        var nextCaves = caveMap[current.Name].Where(x => x.IsLargeCave || x is End || !visited.Contains(x) && x is not Start).ToList();

        foreach (var cave in nextCaves)
        {
            Traverse(cave, visited.ToList());
        }
    }
}

void PartTwo()
{
    var input = File.ReadAllLines("Day12/1.txt").ToList();
    var caveMap = new Dictionary<string, List<Cave>>();
    input.SelectMany(x => x.Split("-"))
        .Distinct().ToList().
        ForEach(caveName =>
        {
            var neighbouringCaves = input.Where(x => x.Split('-')[0] == caveName || x.Split('-')[1] == caveName).SelectMany(x => x.Split('-')).Where(x => x != caveName).Select(x => CreateCave(x)).ToList();
            caveMap.Add(caveName, neighbouringCaves);
        });

    var log = new List<string>();
    Traverse(new Start(), new List<Cave>());
    Console.WriteLine(log.Distinct().ToList().Count);

    void Traverse(Cave current, List< Cave > visited)
{
        visited.Add(current);
        if (current is End)
        {
            log.Add(string.Join(",", visited.Select(x => x.Name)));
            visited = new();
            return;
        }

        var multipleSmallVisits = visited.Any(x => !x.IsLargeCave && visited.Count(y => y == x) == 2);

        var nextCaves = multipleSmallVisits
            ? caveMap[current.Name].Where(x => x.IsLargeCave || x is End || !visited.Contains(x) && x is not Start).ToList()
            : caveMap[current.Name].Where(x => x is not Start).ToList();

        foreach (var cave in nextCaves)
        {
            Traverse(cave, visited.ToList());
        }
    }
}

Cave CreateCave(string name)
{
    switch (name)
    {
        case "start":
            return new Start(name);
        case "end":
            return new End(name);
        default:
            return new Cave(name);
    }
}

internal record Cave(string Name)
{
    public bool IsLargeCave => Name.ToUpper() == Name;
}

internal record Start : Cave
{
    public Start(string Name = "start") : base(Name)
    {
    }
}

internal record End : Cave
{
    public End(string Name = "end") : base(Name)
    {
    }
}