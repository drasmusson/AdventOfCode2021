PartOne();
PartTwo();

void PartOne()
{
    var lines = File.ReadAllLines("Day03/1.txt");

    var gamma = "";
    var epsilon = "";
    for (int i = 0; i < lines[0].Length; i++)
    {
        var average = lines.Select(line => int.Parse(line[i].ToString())).Average();
        if (average >= 0.5) 
        {
            gamma += "1";
            epsilon += "0";
        }
        else
        {
            gamma += "0";
            epsilon += "1";
        }
    }

    var result = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
    Console.WriteLine(result);
}

void PartTwo()
{
    var lines = File.ReadAllLines("Day03/1.txt");

    var oxygenCandidates = new List<string>(lines);
    var co2Candidates = new List<string>(lines);
    for (int i = 0; i < lines[0].Length; i++)
    {
        if (oxygenCandidates.Count > 1)
        {
            var commonOxygenBinary = averageToBitString(oxygenCandidates.Select(line => int.Parse(line[i].ToString())).Average());
            oxygenCandidates = oxygenCandidates.Where(line => line[i] == commonOxygenBinary[0]).ToList();
        }
        if (co2Candidates.Count > 1)
        {
            var commonCo2Binary = averageToBitString(co2Candidates.Select(line => int.Parse(line[i].ToString())).Average());
            co2Candidates = co2Candidates.Where(line => line[i] != commonCo2Binary[0]).ToList();
        }
    }
    var result = Convert.ToInt32(oxygenCandidates[0], 2) * Convert.ToInt32(co2Candidates[0], 2);
    Console.WriteLine(result);
}

static string averageToBitString(double average)
{
    return average >= 0.5 ? "1" : "0";
}