PartOne();
PartTwo();

void PartOne()
{
    var lines = File.ReadAllLines("Day01/1.txt");
    var depthMeassurements = lines.Select(int.Parse).ToList();

    var increases = CountIncreases(depthMeassurements);

    Console.WriteLine(increases);
}

void PartTwo()
{
    var lines = File.ReadAllLines("Day01/1.txt");
    var depthMeassurements = lines.Select(int.Parse).ToList();

    var combinedDepths = new List<int>();

    for (int i = 0; i < depthMeassurements.Count - 2; i++)
    {
        var combined = depthMeassurements[i] + depthMeassurements[i + 1] + depthMeassurements[i + 2];
        combinedDepths.Add(combined);
    }

    var increases = CountIncreases(combinedDepths);
    Console.WriteLine(increases);
}

int CountIncreases(List<int> depths)
{
    var increases = 0;
    var previousMeassurement = depths.First();
    foreach (var combinedDepth in depths)
    {
        if (combinedDepth > previousMeassurement) increases++;

        previousMeassurement = combinedDepth;
    }
    return increases;
}