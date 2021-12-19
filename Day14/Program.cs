//https://adventofcode.com/2021/day/14

using System.Text.RegularExpressions;

PartOne();
PartTwo();

void PartOne()
{
    var lines = File.ReadAllLines("Day14/1.txt");
    var polymerTemplate = lines[0];

    var pairInsertionRules = new Dictionary<string, string>();
    lines.Skip(2).ToList().ForEach(l => pairInsertionRules.Add(l.Split(" -> ")[0], l.Split(" -> ")[1]));

    for (int i = 0; i < 10; i++)
    {
        var newPolymerTemplate = "";
        for (int j = 1; j < polymerTemplate.Length; j++)
        {
            var polymerPart = polymerTemplate[j - 1].ToString() + polymerTemplate[j].ToString();

            if (pairInsertionRules.TryGetValue(polymerPart, out var insertion))
            {
                if (j == 1)
                {
                    polymerPart = polymerPart.Insert(1, insertion);
                }
                else
                {
                    polymerPart = insertion + polymerPart.Substring(1);
                }
            }
            newPolymerTemplate += polymerPart;
        }
        polymerTemplate = newPolymerTemplate;
    }

    var counts = polymerTemplate.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count()).OrderByDescending(x => x.Value);
    var result = counts.First().Value - counts.Last().Value;
    Console.WriteLine(result);
}

void PartTwo()
{
    var lines = File.ReadAllLines("Day14/1.txt");
    var polymerTemplate = lines[0];

    var pairInsertionRules = new Dictionary<string, string>();
    lines.Skip(2).ToList().ForEach(l => pairInsertionRules.Add(l.Split(" -> ")[0], l.Split(" -> ")[1]));

    var pairCounts = new Dictionary<string, long>();
    foreach (var pair in pairInsertionRules.Keys)
    {
        var count = 0;
        if (polymerTemplate.Contains(pair))
        {
            count = Regex.Matches(polymerTemplate, pair).Count;
        }
        pairCounts.Add(pair, count);
    }

    var letterCount = new Dictionary<char, long>();
    foreach (var letter in polymerTemplate)
    {
        if (letterCount.ContainsKey(letter))
        {
            letterCount[letter]++;
        }
        else
        {
            letterCount.Add(letter, 1);
        }
    }

    for (int i = 0; i < 40; i++)
    {
        var pairCountCopy = new Dictionary<string, long>(pairCounts);
        foreach (var pair in pairCountCopy.Keys)
        {
            var count = pairCounts[pair];
            pairCountCopy[pair] -= count;
            var newLetter = pairInsertionRules[pair][0];
            pairCountCopy[pair[0].ToString() + newLetter] += count;
            pairCountCopy[newLetter + pair[1].ToString()] += count;

            if (letterCount.ContainsKey(newLetter))
            {
                letterCount[newLetter] += count;
            }
            else
            {
                letterCount.Add(newLetter, count);
            }
        }
        pairCounts = new Dictionary<string, long>(pairCountCopy);
    }

    var result = letterCount.Max(x => x.Value) - letterCount.Min(x => x.Value);
    Console.WriteLine(result);
}
