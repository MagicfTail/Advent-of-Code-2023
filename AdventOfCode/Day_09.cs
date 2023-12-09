namespace AdventOfCode;

public class Day_09 : BaseDay
{
    private readonly string _input;

    public Day_09()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        StringReader reader = new(_input);

        int total = 0;

        string line = reader.ReadLine();

        Stack<int> lastValues = [];

        while (line != null)
        {
            int[] numbers = line.Split(" ").Select(Int32.Parse).ToArray();

            int length = numbers.Length;
            while (!numbers.All(n => n == 0) && length > 1)
            {
                for (int i = 1; i < length; i++)
                {
                    numbers[i - 1] = numbers[i] - numbers[i - 1];
                }

                lastValues.Push(numbers[length - 1]);
                length -= 1;
            }

            int tmp = 0;
            while (lastValues.Count > 0)
            {
                tmp += lastValues.Pop();
            }

            total += tmp;

            line = reader.ReadLine();
        }

        return new(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        StringReader reader = new(_input);

        int total = 0;

        string line = reader.ReadLine();

        Stack<int> firstValues = [];

        while (line != null)
        {
            int[] numbers = line.Split(" ").Select(Int32.Parse).ToArray();

            int length = numbers.Length;
            while (!numbers[..length].All(n => n == 0) && length > 1)
            {
                firstValues.Push(numbers[0]);

                for (int i = 1; i < length; i++)
                {
                    numbers[i - 1] = numbers[i] - numbers[i - 1];
                }

                length -= 1;
            }

            int tmp = firstValues.Pop();
            while (firstValues.Count > 0)
            {
                tmp = firstValues.Pop() - tmp;
            }

            total += tmp;

            line = reader.ReadLine();
        }

        return new(total.ToString());
    }
}
