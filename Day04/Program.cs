//https://adventofcode.com/2021/day/4

PartOne();
PartTwo();

void PartOne()
{
    var text = File.ReadAllText("Day04/1.txt");
    var chunks = text.Split(new[] { $"{Environment.NewLine}{Environment.NewLine}", "\n\n" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
    var numbersDrawn = chunks[0].Split(',').Select(x => int.Parse(x)).ToList();

    var boardinput = chunks.Skip(1).Select(c => c.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)).ToList();

    var bingoBoards = CreateBingoBoards(boardinput);

    var round = 0;
    while (!bingoBoards.Any(bb => bb.HasBingo))
    {
        var currentNumber = numbersDrawn[round];
        foreach (var bingoBoard in bingoBoards)
        {
            if (bingoBoard.BingoField.Any(x => x.Value.Number == currentNumber))
                bingoBoard.BingoField.FirstOrDefault(x => x.Value.Number == currentNumber).Value.Checked = true;
        }
        round++;
    }

    var winningBoard = bingoBoards.First(bb => bb.HasBingo);
    var fieldSum = winningBoard.BingoField.Where(bf => !bf.Value.Checked).Select(x => x.Value.Number).Sum();
    var winningScore = fieldSum * numbersDrawn[round-1];

    Console.WriteLine(winningScore);
}

void PartTwo()
{
    var text = File.ReadAllText("Day04/1.txt");
    var chunks = text.Split(new[] { $"{Environment.NewLine}{Environment.NewLine}", "\n\n" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
    var numbersDrawn = chunks[0].Split(',').Select(x => int.Parse(x)).ToList();

    var boardinput = chunks.Skip(1).Select(c => c.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)).ToList();

    var bingoBoards = CreateBingoBoards(boardinput);

    var round = 0;
    while (bingoBoards.Any(bb => !bb.HasBingo))
    {
        var currentNumber = numbersDrawn[round];
        foreach (var bingoBoard in bingoBoards)
        {
            if (bingoBoard.HasBingo)
                continue;

            if (bingoBoard.BingoField.Any(x => x.Value.Number == currentNumber))
                bingoBoard.BingoField.FirstOrDefault(x => x.Value.Number == currentNumber).Value.Checked = true;

            if (bingoBoard.HasBingo)
            {
                bingoBoard.BingoRound = round;
                bingoBoard.BingoNumber = currentNumber;
            }
        }
        round++;
    }
    var lastBoard = bingoBoards.OrderBy(x => x.BingoRound).Last();
    var fieldSum = lastBoard.BingoField.Where(bf => !bf.Value.Checked).Select(x => x.Value.Number).Sum();
    var winningScore = fieldSum * lastBoard.BingoNumber;

    Console.WriteLine(winningScore);
}

List<BingoBoard> CreateBingoBoards(List<string[]> boardinput)
{
    var bingoBoards = new List<BingoBoard>();
    foreach (var lines in boardinput)
    {
        var bingoCard = new BingoBoard();
        for (int i = 0; i < 5; i++)
        {
            var intChars = lines[i].Replace("  ", " ").Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < 5; j++)
            {
                bingoCard.BingoField.Add((i, j), new BingoNumber(int.Parse(intChars[j])));
            }
        }
        bingoBoards.Add(bingoCard);
    }
    return bingoBoards;
}

internal class BingoBoard
{
    public int BingoRound { get; set; }
    public int BingoNumber { get; set; }
    public Dictionary<(int X, int Y), BingoNumber> BingoField { get; set; }
    public bool HasBingo => HasBingoCheck();

    public BingoBoard()
    {
        BingoField = new Dictionary<(int X, int Y), BingoNumber>();
    }
    private bool HasBingoCheck()
    {
        
        for (int i = 0; i < 5; i++)
        {
            var xCount = 0;
            var yCount = 0;
            for (int j = 0; j < 5; j++)
            {
                var xvalue = BingoField.GetValueOrDefault((i, j)).Number;
                if (BingoField.GetValueOrDefault((i, j)).Checked) xCount++;

                var yvalue = BingoField.GetValueOrDefault((j, i)).Number;
                if (BingoField.GetValueOrDefault((j, i)).Checked) yCount++;

                if (xCount >= 5 || yCount >= 5) return true;
            }
        }

        return false;
    }
}

internal class BingoNumber
{
    public int Number { get; set; }
    public bool Checked { get; set; }


    public BingoNumber(int number)
    {
        Number = number;
        Checked = false;
    }
}