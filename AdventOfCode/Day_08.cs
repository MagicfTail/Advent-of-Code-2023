namespace AdventOfCode;

using System.Numerics;
using MathNet.Numerics;

public partial class Day_08 : BaseDay
{
    private readonly string _input;

    class RunningInstruction(string instruction, string id)
    {
        public string Instruction = instruction;
        public string Id = id;
        public Dictionary<string, int> Seen = [];
        public int loopSize = -1;
        public int loopStart = -1;
    }

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

        List<RunningInstruction> runningInstructions = map.Keys.Where(i => i[2] == 'A').Select(i =>
        {
            return new RunningInstruction(i, i);
        }).ToList();

        List<RunningInstruction> completed = [];

        int i = 0;
        while (runningInstructions.Count > 0)
        {
            steps += 1;

            char instruction = instructions[i];

            foreach (RunningInstruction ri in runningInstructions)
            {
                ri.Instruction = instruction == 'L' ? map[ri.Instruction][..3] : map[ri.Instruction][3..];

                if (ri.Instruction[2] != 'Z')
                {
                    continue;
                }

                string current = ri.Instruction + i;

                if (ri.Seen.TryGetValue(current, out int value))
                {
                    ri.loopSize = steps - value;
                    ri.loopStart = value;
                    completed.Add(ri);
                }
                else
                {
                    ri.Seen[current] = steps;
                }

                runningInstructions = runningInstructions.Where(ri => ri.loopStart == -1).ToList();
            }

            i = (i + 1) % instructions.Length;
        }

        // They were nice with how the loops are created, so we don't need all the extra stuff we collected
        BigInteger[] loopSizes = completed.Select(ri => new BigInteger(ri.loopSize)).ToArray();

        return new(Euclid.LeastCommonMultiple(loopSizes).ToString());
    }

    [GeneratedRegex(@"(\w+) = \((\w+), (\w+)\)")]
    private static partial Regex MapRegex();
}
