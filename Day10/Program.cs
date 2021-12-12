https://adventofcode.com/2021/day/10
PartOne();
PartTwo();



Console.WriteLine("Hello, World!");

void PartOne()
{
    var input = File.ReadAllLines("Day10/1.txt").ToList();
    var lines = new List<List<ChunkPart>>();
    foreach (var line in input)
    {
        var chunkParts = new List<ChunkPart>();
        foreach (var part in line)
        {
            chunkParts.Add(new ChunkPart(part));
        }
        lines.Add(chunkParts);
    }
    var invalidChunkParts = new List<ChunkPart>();
    foreach (var line in lines)
    {
        var openers = new List<ChunkPart>();
        foreach (var chunkPart in line)
        {
            if (chunkPart.IsOpener)
            {
                openers.Add(chunkPart);
            }
            else 
            {
                if (chunkPart.Value != openers.Last().Closer.Value)
                {
                    invalidChunkParts.Add(chunkPart);
                    break;
                }
                openers.RemoveAt(openers.Count - 1);
            }
        }
    }

    var result = invalidChunkParts.Select(ic => ic.IllegalValue).Sum();
    Console.WriteLine(result);
}

void PartTwo()
{
    var input = File.ReadAllLines("Day10/1.txt").ToList();
    var lines = new List<List<ChunkPart>>();
    foreach (var line in input)
    {
        var chunkParts = new List<ChunkPart>();
        foreach (var part in line)
        {
            chunkParts.Add(new ChunkPart(part));
        }
        lines.Add(chunkParts);
    }
    lines = RemoveIllegalLines(lines);
    var invalidChunkParts = new List<ChunkPart>();
    var lineOpeners = new List<List<ChunkPart>>();
    for (int i = 0; i < lines.Count; i++)
    {
        var openers = new List<ChunkPart>();
        foreach (var chunkPart in lines[i])
        {
            if (chunkPart.IsOpener)
            {
                openers.Add(chunkPart);
            }
            else
            {
                openers.RemoveAt(openers.Count - 1);
            }
        }
        lineOpeners.Add(openers);
    }
    lineOpeners.ForEach(lo => lo.Reverse());
    var lineCloserValues = lineOpeners.Select(lo => lo.Select(c => (long)c.Closer.AutocompleteValue).ToList()).ToList();
    var results = lineCloserValues.Select(lc => lc.Aggregate((a, x) => a * 5 + x)).ToList();
    results.Sort();
    var result = results[results.Count / 2];
    Console.WriteLine(result);
}

List<List<ChunkPart>> RemoveIllegalLines(List<List<ChunkPart>> lines)
{
    var result = new List<List<ChunkPart>>();
    foreach (var line in lines)
    {
        var openers = new List<ChunkPart>();

        if (LineIsValid(line))
            result.Add(line);
    }
    return result;
}

bool LineIsValid(List<ChunkPart> line)
{
    var isValid = true;

    var openers = new List<ChunkPart>();
    foreach (var chunkPart in line)
    {
        if (chunkPart.IsOpener)
        {
            openers.Add(chunkPart);
        }
        else
        {
            if (chunkPart.Value != openers.Last().Closer.Value)
            {
                isValid = false;
                return isValid;
            }
            openers.RemoveAt(openers.Count - 1);
        }
    }
    return isValid;
}

internal record ChunkPart(char Value)
{
    public bool IsOpener => ChunkOpeners.Contains(Value);
    public ChunkPart Closer => GetCloser();
    public int IllegalValue => GetIllegalValue();
    public int AutocompleteValue => GetAutocompleteValue();


    private char[] ChunkOpeners = new char[] { '(', '[', '{', '<' };

    private int GetAutocompleteValue()
    {
        switch (Value)
        {
            case ')':
                return 1;

            case ']':
                return 2;

            case '}':
                return 3;

            case '>':
                return 4;
            default:
                throw new NotSupportedException();
        }
    }

    private int GetIllegalValue()
    {
        switch (Value)
        {
            case ')':
                return 3;

            case ']':
                return 57;

            case '}':
                return 1197;

            case '>':
                return 25137;
            default:
                throw new NotSupportedException();
        }
    }

    private ChunkPart GetCloser()
    {
        switch (Value)
        {
            case '(':
                return new ChunkPart(')');

            case '[':
                return new ChunkPart(']');

            case '{':
                return new ChunkPart('}');

            case '<':
                return new ChunkPart('>');
            default:
                throw new NotSupportedException();
        }
    }
}