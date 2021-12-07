PartOne();
PartTwo();

void PartOne()
{
    var line = File.ReadAllLines("Day07/1.txt")[0];
    var crabPositions = line.Split(',').Select(x => int.Parse(x)).ToList();

    crabPositions.Sort();

    var positionRange = Enumerable.Range(crabPositions[0], crabPositions.Last() - crabPositions[0]).ToList();

    var fuelcostsForPosition = new List<int>();
    foreach (var position in positionRange)
    {
        fuelcostsForPosition.Add(crabPositions.Select(c => Math.Abs(c - position)).Sum());
    }

    var result = fuelcostsForPosition.Min();
    Console.WriteLine(result);
}

void PartTwo()
{
    var line = File.ReadAllLines("Day07/1.txt")[0];
    var crabPositions = line.Split(',').Select(x => int.Parse(x)).ToList();

    crabPositions.Sort();

    var positionRange = Enumerable.Range(crabPositions[0], crabPositions.Last() - crabPositions[0]).ToList();

    var fuelcostsForPosition = new List<int>();
    foreach (var position in positionRange)
    {
        fuelcostsForPosition.Add(crabPositions.Select(c => GetSum(1, Math.Abs(c - position))).Sum());
    }
    var result = fuelcostsForPosition.Min();
    Console.WriteLine(result);

    int GetSum(int a, int b)
    {
        return (b * (b + 1) - (a - 1) * a) / 2;
    }
}
