https://adventofcode.com/2021/day/8

PartOne();

void PartOne()
{
    var lines = File.ReadAllLines("Day08/1.txt");
    var outputDigits = lines.Select(line =>
        line.Split('|').Select(x =>
        x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        .Last()
        .ToList());

    var numberOf1478 = 0;

    foreach (var outputs in outputDigits)
    {
        numberOf1478 += outputs.Count(o =>
            o.Length == 2 ||
            o.Length == 3 ||
            o.Length == 4 ||
            o.Length == 7);
    }

    Console.WriteLine(numberOf1478);
}