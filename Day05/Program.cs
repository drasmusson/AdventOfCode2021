PartOne();
PartTwo();

void PartOne()
{
    var lines = File.ReadAllLines("Day05/1.txt");
    var inputCoordinates = new List<((int, int), (int, int))>();
    foreach (var line in lines)
    {
        var decimalStrings = line.Split(" -> ").ToList();
        var p = decimalStrings.Select(s => (int.Parse(s.Split(",")[0]), int.Parse(s.Split(",")[1]))).ToList();
        inputCoordinates.Add(((p[0].Item1, p[0].Item2), (p[1].Item1, p[1].Item2)));
    }

    inputCoordinates = inputCoordinates.Where(ic => (ic.Item1.Item1 == ic.Item2.Item1 || ic.Item1.Item2 == ic.Item2.Item2)).ToList();
    
    var coordinates = new List<(int, int)>();
    foreach (var inputCoordinate in inputCoordinates)
    {
        var coordinatesToAdd = GetCoordinateRange(inputCoordinate.Item1, inputCoordinate.Item2);
        coordinates.AddRange(coordinatesToAdd);
    }

    var groups = coordinates.GroupBy(c => c);
    var result = groups.Count(g => g.Count() > 1);

    Console.WriteLine(result);
}

void PartTwo()
{
    var lines = File.ReadAllLines("Day05/1.txt");
    var inputCoordinates = new List<((int, int), (int, int))>();
    foreach (var line in lines)
    {
        var decimalStrings = line.Split(" -> ").ToList();
        var p = decimalStrings.Select(s => (int.Parse(s.Split(",")[0]), int.Parse(s.Split(",")[1]))).ToList();
        inputCoordinates.Add(((p[0].Item1, p[0].Item2), (p[1].Item1, p[1].Item2)));
    }

    var coordinates = new List<(int, int)>();
    foreach (var inputCoordinate in inputCoordinates)
    {
        var coordinatesToAdd = GetCoordinateRange(inputCoordinate.Item1, inputCoordinate.Item2);
        coordinates.AddRange(coordinatesToAdd);
    }

    var groups = coordinates.GroupBy(c => c);
    var result = groups.Count(g => g.Count() > 1);

    Console.WriteLine(result);
}

List<(int, int)> GetCoordinateRange((int X, int Y) coordinateA, (int X, int Y) coordinateB)
{
    var coordinates = new List<(int, int)>();
    var xNumbers = new List<int> { coordinateA.X, coordinateB.X };
    xNumbers.Sort();
    var yNumbers = new List<int> { coordinateA.Y, coordinateB.Y };
    yNumbers.Sort();

    var xRange = Enumerable.Range(xNumbers[0], xNumbers[1] - xNumbers[0] + 1).ToList();
    if (coordinateA.X > coordinateB.X)
    {
        xRange.Reverse();
    }

    var yRange = Enumerable.Range(yNumbers[0], yNumbers[1] - yNumbers[0] + 1).ToList();
    if (coordinateA.Y > coordinateB.Y)
    {
        xRange.Reverse();
    }
    var iterations = xRange.Count == 1 ? yRange.Count : xRange.Count;
    for (int i = 0; i < iterations; i++)
    {

        if (xRange.Count == 1)
        {
            coordinates.Add((xRange[0], yRange[i]));

        }
        else if (yRange.Count == 1)
        {
            coordinates.Add((xRange[i], yRange[0]));

        }
        else
        {
            coordinates.Add((xRange[i], yRange[i]));
        }
    }

    return coordinates;
}