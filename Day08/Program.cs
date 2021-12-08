https://adventofcode.com/2021/day/8

PartOne();
PartTwo();

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

void PartTwo()
{
    var lines = File.ReadAllLines("Day08/1.txt");
    var displays = lines.Select(line =>
        line.Split('|').Select(x =>
        x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        .ToList())
        .ToList()
        .Select(i => new Display(i))
        .ToList();

    var result = displays.Sum(d => d.Output);
    Console.WriteLine(result);
}

internal class Display
{
    public int Output { get; private set; }
    public List<DigitTranslation> Translations { get; set; }

    public Display(List<string[]> input)
    {
        input[1].ToList();
        Translations = new List<DigitTranslation>();
        CreateTranslations(input[0].ToList());

        var outputString = "";
        foreach (var displayString in input[1].ToList())
        {
            TryTranslate(displayString, out var digit);
            outputString += digit.ToString();
        }
        Output = int.Parse(outputString);
    }

    private void CreateTranslations(List<string> inputStrings)
    {
        foreach (var inputString in inputStrings)
        {
            switch (inputString.Length)
            {
                case 2:
                    Translations.Add(new DigitTranslation(inputString, 1));
                    break;
                case 3:
                    Translations.Add(new DigitTranslation(inputString, 7));
                    break;
                case 4:
                    Translations.Add(new DigitTranslation(inputString, 4));
                    break;
                case 7:
                    Translations.Add(new DigitTranslation(inputString, 8));
                    break;
                default:
                    break;
            }
        }

        do
        {
            foreach (var inputString in inputStrings)
            {
                if (!TryTranslate(inputString, out var digit))
                {
                    if (inputString.Length == 5)
                    {
                        var one = Translations.FirstOrDefault(x => x.Value == 1);
                        if (one.Key.All(c => inputString.Contains(c)))
                        {
                            Translations.Add(new DigitTranslation(inputString, 3));
                            continue;
                        }
                        var fourSeven = Translations.FirstOrDefault(x => x.Value == 4).Key + Translations.FirstOrDefault(x => x.Value == 7).Key;
                        if (inputString.Count(c => !fourSeven.Contains(c)) > 1)
                        {
                            Translations.Add(new DigitTranslation(inputString, 2));
                            continue;
                        }
                        Translations.Add(new DigitTranslation(inputString, 5));
                        continue;

                    }
                    else if (inputString.Length == 6)
                    {
                        var one = Translations.FirstOrDefault(x => x.Value == 1);
                        if (!one.Key.All(c => inputString.Contains(c)))
                        {
                            Translations.Add(new DigitTranslation(inputString, 6));
                            continue;
                        }
                        var four = Translations.FirstOrDefault(x => x.Value == 4);
                        if (inputString.Count(c => !four.Key.Contains(c)) == 2)
                        {
                            Translations.Add(new DigitTranslation(inputString, 9));
                            continue;
                        }
                        Translations.Add(new DigitTranslation(inputString, 0));
                        continue;
                    }
                }
            }
        } while (Translations.Count < 10);
    }
    private bool TryTranslate(string key, out int digit)
    {
        digit = 0;
        foreach (var translation in Translations)
        {
            if (translation.TryGetValueFromKey(key, out var value))
            {
                digit = value;
                return true;
            }
        }
        return false;
    }
}

internal class DigitTranslation
{
    public string Key { get; private set; }
    public int Value { get; private set; }

    public DigitTranslation(string key, int value)
    {
        Key = key;
        Value = value;
    }

    public bool TryGetValueFromKey(string key, out int value)
    {
        var match = false;
        if (key.Length == Key.Length)
        {
            match = key.All(x => Key.Contains(x)) && Key.All(x => key.Contains(x));
        }

        value = Value;
        return match;
    }
}