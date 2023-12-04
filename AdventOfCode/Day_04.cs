namespace AdventOfCode;

public class Day_04 : BaseDay
{
    private readonly string _input;

    public Day_04()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        StringReader reader = new(_input);

        int total = 0;

        HashSet<int> seen = [];
        string line = reader.ReadLine();
        int lineNumber = 0;

        while (line != null)
        {
            string currentNumber = "";
            int split = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ':')
                {
                    split = i + 1;
                    break;
                }
            }

            for (int i = split; i < line.Length; i++)
            {
                char c = line[i];

                if (char.IsDigit(c))
                {
                    currentNumber += c;
                }
                else if (c == '|')
                {
                    split = i + 1;
                    break;
                }
                else if (currentNumber != "")
                {
                    seen.Add(Int32.Parse(currentNumber));
                    currentNumber = "";
                }
            }

            int winning = 0;
            for (int j = split; j < line.Length; j++)
            {
                char c = line[j];

                if (char.IsDigit(c))
                {
                    currentNumber += c;
                }
                else if (currentNumber != "")
                {
                    if (seen.Contains(Int32.Parse(currentNumber)))
                    {
                        winning += 1;
                    }
                    currentNumber = "";
                }
            }

            if (seen.Contains(Int32.Parse(currentNumber)))
            {
                winning += 1;
            }

            if (winning > 0)
            {
                total += (int)Math.Pow(2, winning - 1);
            }

            lineNumber += 1;
            line = reader.ReadLine();
            seen = [];
        }

        return new(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }
}
