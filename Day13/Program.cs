//https://adventofcode.com/2021/day/13
PartOne();
PartTwo();

void PartOne()
{
    var input = File.ReadAllLines("Day13/1.txt");
    var dots = input.Where(x => !x.Contains("fold") && x != "").Select(x => new Coord(int.Parse(x.Split(",")[0]), int.Parse(x.Split(",")[1]))).ToHashSet();
    var foldOrder = input.Where(x => x.Contains("fold")).Select(x => new FoldOrder(x.Split("fold along ")[1])).First();

    Fold(foldOrder, ref dots);
    Console.WriteLine(dots.Count);
}

void PartTwo()
{
    var input = File.ReadAllLines("Day13/1.txt");
    var dots = input.Where(x => !x.Contains("fold") && x != "").Select(x => new Coord(int.Parse(x.Split(",")[0]), int.Parse(x.Split(",")[1]))).ToHashSet();
    var foldOrders = input.Where(x => x.Contains("fold")).Select(x => new FoldOrder(x.Split("fold along ")[1])).ToList();

    foreach (var foldOrder in foldOrders)
    {
        Fold(foldOrder, ref dots);
    }

    PrintGrid(dots);
}

void Fold(FoldOrder foldOrder, ref HashSet<Coord> dots)
{
    var dotsOverCrease = new List<Coord>();

    if (foldOrder.Axis == Axis.Y)
    {
        dotsOverCrease = dots.Where(d => d.Y > foldOrder.Location).ToList();

        foreach (var dot in dotsOverCrease)
        {
            var dotToAdd = dot with { Y = foldOrder.Location - (dot.Y - foldOrder.Location) };
            dots.Add(dotToAdd);
        }
    }
    else
    {
        dotsOverCrease = dots.Where(d => d.X > foldOrder.Location).ToList();

        foreach (var dot in dotsOverCrease)
        {
            var dotToAdd = dot with { X = foldOrder.Location - (dot.X - foldOrder.Location) };
            dots.Add(dotToAdd);
        }
    }
    dots = dots.Except(dotsOverCrease).ToHashSet();
}

void PrintGrid(HashSet<Coord> dots)
{
    int maxCol = dots.Max(c => c.X) + 1;
    int maxRow = dots.Max(c => c.Y) + 1;

    for (int y = 0; y < maxRow; y++)
    {
        var rowToPrint = "";
        for (int x = 0; x < maxCol; x++)
            rowToPrint += dots.Contains(new Coord(x, y)) ? '#' : ' ';
        Console.WriteLine(rowToPrint);
    }
    Console.WriteLine("");
}

internal record Coord(int X, int Y);

internal class FoldOrder
{
    public Axis Axis { get; private set; }
    public int Location { get; private set; }
    public FoldOrder(string input)
    {
        var split = input.Split("=");
        switch (split[0])
        {
            case "x":
                Axis = Axis.X;
                break;
            case "y":
                Axis = Axis.Y;
                break;
            default:
                break;
        }
        Location = int.Parse(split[1]);
    }
}

enum Axis
{
    X,
    Y
}