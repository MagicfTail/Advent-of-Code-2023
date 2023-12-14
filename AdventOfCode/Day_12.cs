namespace AdventOfCode;

public partial class Day_12 : BaseDay
{
    private readonly string _input;

    public Day_12()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        static int RecPossibilities(int previousDamaged, string input, int[] missingRanges)
        {
            int damaged = previousDamaged;

            if (input.Length == 0)
            {
                if (missingRanges.Length == 0 && damaged == 0)
                {
                    return 1;
                }
                else if (missingRanges.Length == 1 && missingRanges[0] == damaged)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            for (int i = 0; i < input.Length; i++)
            {
                char current = input[i];

                if (damaged > 0 && (missingRanges.Length == 0 || damaged > missingRanges[0]))
                {
                    return 0;
                }


                switch (current)
                {
                    case '#':
                        damaged += 1;
                        break;
                    case '.':
                        if (damaged == 0)
                        {
                            continue;
                        }
                        else if (missingRanges.Length == 0 || damaged != missingRanges[0])
                        {
                            return 0;
                        }
                        else
                        {
                            missingRanges = missingRanges[1..];
                            damaged = 0;
                        }
                        break;
                    case '?':
                        if (damaged == 0)
                        {
                            return RecPossibilities(0, input[(i + 1)..], missingRanges) +
                                   RecPossibilities(damaged + 1, input[(i + 1)..], missingRanges);
                        }
                        else if (missingRanges.Length > 0 && missingRanges[0] == damaged)
                        {
                            return RecPossibilities(0, input[(i + 1)..], missingRanges[1..]) +
                                   RecPossibilities(damaged + 1, input[(i + 1)..], missingRanges);
                        }
                        else
                        {
                            return RecPossibilities(damaged + 1, input[(i + 1)..], missingRanges);
                        }
                }
            }

            if ((missingRanges.Length == 0 && damaged == 0) ||
                (missingRanges.Length == 1 && damaged == missingRanges[0]))
            {
                return 1;
            }

            return 0;
        }

        Regex digitRegex = DigitRegex();

        StringReader reader = new(_input);

        int total = 0;

        string line = reader.ReadLine();

        while (line != null)
        {
            string[] parts = line.Split(' ');

            string input = parts[0];
            int[] ranges = digitRegex.Matches(parts[1]).Select(m => Int32.Parse(m.Value)).ToArray();

            total += RecPossibilities(0, input, ranges);

            line = reader.ReadLine();
        }

        return new(total.ToString());
    }

    // Not mine. Small bug in mine that I couldn't be bothered to find, bu same idea
    public override ValueTask<string> Solve_2()
    {
        Dictionary<string, long> cache = [];
        long Calculate(string springs, List<int> groups)
        {
            var key = $"{springs},{string.Join(',', groups)}";  // Cache key: spring pattern + group lengths

            if (cache.TryGetValue(key, out var value))
            {
                return value;
            }

            value = GetCount(springs, groups);
            cache[key] = value;

            return value;
        }

        long GetCount(string springs, List<int> groups)
        {
            while (true)
            {
                if (groups.Count == 0)
                {
                    return springs.Contains('#') ? 0 : 1; // No more groups to match: if there are no springs left, we have a match
                }

                if (string.IsNullOrEmpty(springs))
                {
                    return 0; // No more springs to match, although we still have groups to match
                }

                if (springs.StartsWith('.'))
                {
                    springs = springs.Trim('.'); // Remove all dots from the beginning
                    continue;
                }

                if (springs.StartsWith('?'))
                {
                    return Calculate("." + springs[1..], groups) + Calculate("#" + springs[1..], groups); // Try both options recursively
                }

                if (springs.StartsWith('#')) // Start of a group
                {
                    if (groups.Count == 0)
                    {
                        return 0; // No more groups to match, although we still have a spring in the input
                    }

                    if (springs.Length < groups[0])
                    {
                        return 0; // Not enough characters to match the group
                    }

                    if (springs[..groups[0]].Contains('.'))
                    {
                        return 0; // Group cannot contain dots for the given length
                    }

                    if (groups.Count > 1)
                    {
                        if (springs.Length < groups[0] + 1 || springs[groups[0]] == '#')
                        {
                            return 0; // Group cannot be followed by a spring, and there must be enough characters left
                        }

                        springs = springs[(groups[0] + 1)..]; // Skip the character after the group - it's either a dot or a question mark
                        groups = groups[1..];
                        continue;
                    }

                    springs = springs[groups[0]..]; // Last group, no need to check the character after the group
                    groups = groups[1..];
                    continue;
                }

                throw new Exception("Invalid input");
            }
        }

        Regex digitRegex = DigitRegex();

        StringReader reader = new(_input);

        long total = 0;

        string line = reader.ReadLine();

        while (line != null)
        {
            string[] parts = line.Split(' ');

            string input = parts[0];
            int[] ranges = digitRegex.Matches(parts[1]).Select(m => Int32.Parse(m.Value)).ToArray();

            string input2 = string.Join('?', Enumerable.Repeat(input, 5));
            List<int> ranges2 = Enumerable.Repeat(ranges, 5).SelectMany(g => g).ToList();

            total += Calculate(input2, ranges2);

            line = reader.ReadLine();
        }

        return new(total.ToString());
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex DigitRegex();
}
