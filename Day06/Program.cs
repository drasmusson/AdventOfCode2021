PartOne();
PartTwo();

Console.WriteLine("Hello, World!");

void PartOne()
{
    var line = File.ReadAllLines("Day06/1.txt")[0];
    var fishInput = line.Split(',').Select(x => int.Parse(x)).ToList();
    var lanternFishes = fishInput.Select(f => new LanternFish(f)).ToList();

    for (int i = 0; i < 80; i++)
    {
        var fishesToAdd = new List<LanternFish>();
        foreach (var lanternFish in lanternFishes)
        {
            if (lanternFish.InternalTimer == 0)
            {
                fishesToAdd.Add(lanternFish.Reset());
            }
            else
            {
                lanternFish.InternalTimer--;
            }
        }
        lanternFishes.AddRange(fishesToAdd);
    }

    Console.WriteLine(lanternFishes.Count);
}

void PartTwo()
{
    var line = File.ReadAllLines("Day06/1.txt")[0];
    var fishes = line.Split(',').Select(x => int.Parse(x)).ToList();
    var fishGroups = fishes.GroupBy(f => f).ToList();

    var timerCounts = fishGroups.ToDictionary(g => g.Key, g => (long)g.Count());

    for (int i = 0; i < 256; i++)
    {
        var newTimerCounts = timerCounts.ToDictionary(tc => tc.Key, tc => tc.Value);

        newTimerCounts[8] = timerCounts.GetValueOrDefault(0);
        newTimerCounts[7] = timerCounts.GetValueOrDefault(8);
        newTimerCounts[6] = timerCounts.GetValueOrDefault(7) + timerCounts.GetValueOrDefault(0);
        newTimerCounts[5] = timerCounts.GetValueOrDefault(6);
        newTimerCounts[4] = timerCounts.GetValueOrDefault(5);
        newTimerCounts[3] = timerCounts.GetValueOrDefault(4);
        newTimerCounts[2] = timerCounts.GetValueOrDefault(3);
        newTimerCounts[1] = timerCounts.GetValueOrDefault(2);
        newTimerCounts[0] = timerCounts.GetValueOrDefault(1);

        timerCounts = newTimerCounts.ToDictionary(tc => tc.Key, tc => tc.Value);
    }

    Console.WriteLine(timerCounts.Values.Sum());
}

internal class LanternFish
{
    public int InternalTimer { get; set; }

    public LanternFish()
    {
        InternalTimer = 8;
    }

    public LanternFish(int initCounter)
    {
        InternalTimer = initCounter;
    }

    public LanternFish Reset()
    {
        InternalTimer = 6;

        return new LanternFish();
    }
}