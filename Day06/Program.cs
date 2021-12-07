//https://adventofcode.com/2021/day/6

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

    var fishTimers = fishGroups.ToDictionary(g => g.Key, g => (long)g.Count());

    for (int i = 0; i < 256; i++)
    {
        var newTimerCounts = fishTimers.ToDictionary(tc => tc.Key, tc => tc.Value);

        newTimerCounts[8] = fishTimers.GetValueOrDefault(0);
        newTimerCounts[7] = fishTimers.GetValueOrDefault(8);
        newTimerCounts[6] = fishTimers.GetValueOrDefault(7) + fishTimers.GetValueOrDefault(0);
        newTimerCounts[5] = fishTimers.GetValueOrDefault(6);
        newTimerCounts[4] = fishTimers.GetValueOrDefault(5);
        newTimerCounts[3] = fishTimers.GetValueOrDefault(4);
        newTimerCounts[2] = fishTimers.GetValueOrDefault(3);
        newTimerCounts[1] = fishTimers.GetValueOrDefault(2);
        newTimerCounts[0] = fishTimers.GetValueOrDefault(1);

        fishTimers = newTimerCounts.ToDictionary(tc => tc.Key, tc => tc.Value);
    }

    Console.WriteLine(fishTimers.Values.Sum());
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