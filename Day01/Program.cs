PartOne();
PartTwo();

void PartOne()
{
    var lines = File.ReadAllLines("Day01/1.txt");
    var depthMeassurements = lines.Select(int.Parse).ToArray();

    var increases = 0;
    var previousMeassurement = depthMeassurements.First();
    foreach (var depthMeassurement in depthMeassurements)
    {
        if (depthMeassurement > previousMeassurement) increases++;

        previousMeassurement = depthMeassurement;
    }

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

    var increases = 0;
    var previousMeassurement = combinedDepths.First();
    foreach (var combinedDepth in combinedDepths)
    {
        if (combinedDepth > previousMeassurement) increases++;

        previousMeassurement = combinedDepth;
    }

    Console.WriteLine(increases);
}