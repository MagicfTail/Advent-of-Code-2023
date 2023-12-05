namespace AdventOfCode;

public partial class Day_05 : BaseDay
{
    private readonly string _input;

    public struct Map(Int64 dest, Int64 source, Int64 range)
    {
        public Int64 sourceStart = source;
        public Int64 sourceEnd = source + range - 1;
        private readonly Int64 offset = dest - source;

        public readonly bool InRange(Int64 value)
        {
            return value >= sourceStart && value <= sourceEnd;
        }

        public readonly Int64 GetMapped(Int64 value)
        {
            return value + offset;
        }

        public readonly Range GetMappedRangeStart(Range value)
        {
            return new Range(value._start + offset, sourceEnd + offset);
        }

        public readonly Range GetMappedRangeEnd(Range value)
        {
            return new Range(sourceStart + offset, value._end + offset);
        }

        public readonly Range GetMappedRangeBoth(Range value)
        {
            return new Range(value._start + offset, value._end + offset);
        }
    }

    public struct Range(Int64 start, Int64 end)
    {
        public Int64 _start = start;
        public Int64 _end = end;
    }

    public Day_05()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        StringReader reader = new(_input + "\n\n");
        Regex mapRegex = MapRegex();
        Regex seedsRegex = SeedsRegex();

        string line = reader.ReadLine();

        // Hardcode the different maps, could easily make it pretty generic

        MatchCollection seedMatches = seedsRegex.Matches(line);

        var seeds = seedMatches.Select(x => Int64.Parse(x.Value)).ToArray();

        List<Map> maps = [];

        line = reader.ReadLine();
        while (line != null)
        {
            if (line == "")
            {
                seeds = seeds.Select(seed =>
                {
                    foreach (Map map in maps)
                    {
                        if (map.InRange(seed))
                        {
                            return map.GetMapped(seed);
                        }
                    }
                    return seed;
                }).ToArray();

                line = reader.ReadLine();
                maps = [];
            }
            else
            {
                Match match = mapRegex.Match(line);
                maps.Add(new Map(Int64.Parse(match.Groups[1].Value),
                                 Int64.Parse(match.Groups[2].Value),
                                 Int64.Parse(match.Groups[3].Value)));
            }
            line = reader.ReadLine();
        }

        return new(seeds.Min().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        StringReader reader = new(_input + "\n\n");
        Regex mapRegex = MapRegex();
        Regex seedsRegex = SeedsRangeRegex();

        string line = reader.ReadLine();

        // Hardcode the different maps, could easily make it pretty generic

        MatchCollection seedMatches = seedsRegex.Matches(line);

        var seeds = seedMatches.Select(x =>
        {
            Int64 start = Int64.Parse(x.Groups[1].Value);
            Int64 range = Int64.Parse(x.Groups[2].Value);

            return new Range(start, start + range - 1);
        }).ToArray();

        List<Map> maps = [];

        line = reader.ReadLine();
        while (line != null)
        {
            if (line == "")
            {
                Queue<Range> q = new(seeds);
                List<Range> temp = [];

                while (q.Count > 0)
                {
                    Range current = q.Dequeue();
                    bool handled = false;

                    foreach (Map map in maps)
                    {
                        if (map.InRange(current._start) && map.InRange(current._end))
                        {
                            temp.Add(map.GetMappedRangeBoth(current));
                            handled = true;
                            break;
                        }
                        else if (map.InRange(current._start))
                        {
                            temp.Add(map.GetMappedRangeStart(current));
                            q.Enqueue(new Range(map.sourceEnd + 1, current._end));
                            handled = true;
                            break;
                        }
                        else if (map.InRange(current._end))
                        {
                            temp.Add(map.GetMappedRangeEnd(current));
                            q.Enqueue(new Range(current._start, map.sourceStart - 1));
                            handled = true;
                            break;
                        }
                    }

                    if (!handled)
                    {
                        temp.Add(current);
                    }
                }

                seeds = [.. temp];

                line = reader.ReadLine();
                maps = [];
            }
            else
            {
                Match match = mapRegex.Match(line);
                maps.Add(new Map(Int64.Parse(match.Groups[1].Value),
                                 Int64.Parse(match.Groups[2].Value),
                                 Int64.Parse(match.Groups[3].Value)));
            }
            line = reader.ReadLine();
        }

        return new(seeds.Select(x => x._start).Min().ToString());
    }

    [GeneratedRegex(@"(\d+) (\d+) (\d+)")]
    private static partial Regex MapRegex();

    [GeneratedRegex(@"(\d+)")]
    private static partial Regex SeedsRegex();

    [GeneratedRegex(@"(\d+) (\d+)")]
    private static partial Regex SeedsRangeRegex();
}
