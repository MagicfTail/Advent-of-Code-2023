namespace AdventOfCode;

public partial class Day_08 : BaseDay
{
    private readonly string _input;

    public Day_08()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        StringReader reader = new(_input);

        Regex mapRegex = MapRegex();

        Dictionary<string, string> map = [];

        string instructions = reader.ReadLine();
        reader.ReadLine();

        string line = reader.ReadLine();

        while (line != null)
        {
            Match match = mapRegex.Match(line);

            map.Add(match.Groups[1].Value, match.Groups[2].Value + match.Groups[3].Value);

            line = reader.ReadLine();
        }

        int steps = 0;

        string instruction = "AAA";
        int i = 0;
        while (true)
        {
            steps += 1;

            string leftRight = map[instruction];
            instruction = instructions[i] == 'L' ? leftRight[..3] : leftRight[3..];

            if (instruction == "ZZZ")
            {
                break;
            }

            i = (i + 1) % instructions.Length;
        }

        return new(steps.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        StringReader reader = new(_input);

        Regex mapRegex = MapRegex();

        Dictionary<string, string> map = [];

        string instructions = reader.ReadLine();
        reader.ReadLine();

        string line = reader.ReadLine();

        while (line != null)
        {
            Match match = mapRegex.Match(line);

            map.Add(match.Groups[1].Value, match.Groups[2].Value + match.Groups[3].Value);

            line = reader.ReadLine();
        }

        int steps = 0;

        List<Tuple<string, string, HashSet<string>>> runningInstructions = map.Keys.Where(i => i[2] == 'A').Select(i =>
        {
            return new Tuple<string, string, HashSet<string>>(i, i, []);
        }).ToList();

        int i = 0;
        while (true)
        {
            steps += 1;

            char instruction = instructions[i];

            string[] temp = new string[runningInstructions.Length];

            for (int j = 0; j < temp.Length)
                runningInstructions = runningInstructions.Select(i =>
                {
                    return instruction == 'L' ? map[i][..3] : map[i][3..];
                });

            if (runningInstructions.All(s => s[2] == 'Z'))
            {
                break;
            }

            i = (i + 1) % instructions.Length;
        }

        return new(steps.ToString());
    }

    [GeneratedRegex(@"(\w+) = \((\w+), (\w+)\)")]
    private static partial Regex MapRegex();
}
