namespace AdventOfCode;

public class Day_03 : BaseDay
{
    private readonly string _input;

    public Day_03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int[] moves = [-1, 0, 1];
        StringReader reader = new(_input);

        string[] input = _input.Split(Environment.NewLine);

        string currentNumber = "";
        bool validNumber = false;
        int total = 0;

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                char c = input[i][j];

                if (char.IsDigit(c))
                {
                    currentNumber += c;

                    foreach (int row in moves)
                    {
                        foreach (int col in moves)
                        {
                            int rowCheck = i + row;
                            int colCheck = j + col;

                            if (rowCheck < 0 ||
                                colCheck < 0 ||
                                rowCheck >= input.Length ||
                                colCheck >= input[0].Length ||
                                input[rowCheck][colCheck] == '.' ||
                                char.IsDigit(input[rowCheck][colCheck]))
                            {
                                continue;
                            }

                            validNumber = true;
                        }
                    }

                }
                else
                {
                    if (currentNumber != "" && validNumber)
                    {
                        total += Int32.Parse(currentNumber);
                    }

                    currentNumber = "";
                    validNumber = false;
                }
            }
            if (currentNumber != "" && validNumber)
            {
                total += Int32.Parse(currentNumber);
            }

            currentNumber = "";
            validNumber = false;
        }

        return new(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int[] moves = [-1, 0, 1];
        StringReader reader = new(_input);

        string[] input = _input.Split(Environment.NewLine);

        string currentNumber = "";
        HashSet<Tuple<int, int>> stars = [];
        Dictionary<Tuple<int, int>, List<int>> starMatches = [];

        int total = 0;

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                char c = input[i][j];

                if (char.IsDigit(c))
                {
                    currentNumber += c;

                    foreach (int row in moves)
                    {
                        foreach (int col in moves)
                        {
                            int rowCheck = i + row;
                            int colCheck = j + col;

                            if (rowCheck < 0 ||
                                colCheck < 0 ||
                                rowCheck >= input.Length ||
                                colCheck >= input[0].Length)
                            {
                                continue;
                            }

                            if (input[rowCheck][colCheck] == '*')
                            {
                                stars.Add(new Tuple<int, int>(rowCheck, colCheck));
                            }
                        }
                    }

                }
                else
                {
                    foreach (Tuple<int, int> star in stars)
                    {
                        if (!starMatches.TryGetValue(star, out List<int> value))
                        {
                            value = ([]);
                            starMatches[star] = value;
                        }

                        value.Add(Int32.Parse(currentNumber));
                    }

                    currentNumber = "";
                    stars = [];
                }
            }
            foreach (Tuple<int, int> star in stars)
            {
                starMatches[star].Add(Int32.Parse(currentNumber));
            }

            currentNumber = "";
            stars = [];
        }

        foreach (Tuple<int, int> star in starMatches.Keys)
        {
            List<int> numbers = starMatches[star];

            int temp = 1;

            if (numbers.Count == 2)
            {
                foreach (int n in numbers)
                {
                    temp *= n;
                }

                total += temp;
            }
        }

        return new(total.ToString());
    }
}
