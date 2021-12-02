PartOne();
PartTwo();

void PartOne()
{
    var lines = File.ReadAllLines("Day02/1.txt");
    var commands = lines.Select(line => (direction: line.Split(" ")[0], steps: int.Parse(line.Split(" ")[1])));

    var horizontal = 0;
    var depth = 0;

    foreach (var command in commands)
    {
        switch (command.direction)
        {
            case "forward":
                horizontal += command.steps;
                break;

            case "up":
                depth -= command.steps;
                break;

            case "down":
                depth += command.steps;
                break;

            default:
                break;
        }
    }

    var answer = horizontal * depth;
    Console.WriteLine(answer);
}

void PartTwo()
{
    var lines = File.ReadAllLines("Day02/1.txt");
    var commands = lines.Select(line => (direction: line.Split(" ")[0], steps: int.Parse(line.Split(" ")[1])));

    var horizontal = 0;
    var depth = 0;
    var aim = 0;

    foreach (var command in commands)
    {
        switch (command.direction)
        {
            case "forward":
                horizontal += command.steps;
                depth += (aim * command.steps);
                break;

            case "up":
                aim -= command.steps;
                break;

            case "down":
                aim += command.steps;
                break;

            default:
                break;
        }
    }

    var answer = horizontal * depth;
    Console.WriteLine(answer);
}