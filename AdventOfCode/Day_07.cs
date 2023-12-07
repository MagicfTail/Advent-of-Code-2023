namespace AdventOfCode;

public class Day_07 : BaseDay
{
    private readonly string _input;

    public Day_07()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    struct Hand(string cards, int bet)
    {
        public string Cards = cards;
        public int Bet = bet;
    }

    public override ValueTask<string> Solve_1()
    {
        char[] cardTypes = ['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'];

        static int HandStrength(string cards)
        {
            Dictionary<char, int> labelCounts = [];

            foreach (char c in cards)
            {
                labelCounts[c] = labelCounts.GetValueOrDefault(c, 0) + 1;
            }

            if (labelCounts.Values.Count == 1)
            {
                return 0;
            }
            else if (labelCounts.Values.Max() == 4)
            {
                return 1;
            }
            else if (labelCounts.ContainsValue(3) && labelCounts.ContainsValue(2))
            {
                return 2;
            }
            else if (labelCounts.Values.Max() == 3)
            {
                return 3;
            }
            else if (labelCounts.Values.Count == 3)
            {
                return 4;
            }
            else if (labelCounts.Values.Count == 4)
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }

        StringReader reader = new(_input);
        List<Hand> hands = [];

        string line = reader.ReadLine();

        while (line != null)
        {
            hands.Add(new Hand(line[..5], Int32.Parse(line[6..])));

            line = reader.ReadLine();
        }

        Hand[] handsArray = [.. hands];
        Array.Sort(handsArray, (a, b) =>
        {
            int aStrength = HandStrength(a.Cards);
            int bStrength = HandStrength(b.Cards);

            int diff = bStrength - aStrength;
            if (diff != 0)
            {
                return diff;
            }

            for (int i = 0; i < a.Cards.Length; i++)
            {
                diff = Array.IndexOf(cardTypes, b.Cards[i]) - Array.IndexOf(cardTypes, a.Cards[i]);
                if (diff != 0)
                {
                    return diff;
                }
            }

            return 0;
        });

        int total = 0;

        for (int i = 0; i < handsArray.Length; i++)
        {
            total += (i + 1) * handsArray[i].Bet;
        }

        return new(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        char[] cardTypes = ['A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'];

        static int HandStrength(string cards)
        {
            Dictionary<char, int> labelCounts = [];
            int jokers = 0;

            foreach (char c in cards)
            {
                if (c == 'J')
                {
                    jokers += 1;
                }
                else
                {
                    labelCounts[c] = labelCounts.GetValueOrDefault(c, 0) + 1;
                }
            }

            if (labelCounts.Count <= 1)
            {
                return 0;
            }
            else if (labelCounts.Values.Max() + jokers == 4)
            {
                return 1;
            }
            else if (labelCounts.Count == 2 && jokers == 1 ||
                     labelCounts.ContainsValue(3) && labelCounts.ContainsValue(2))
            {
                return 2;
            }
            else if (labelCounts.Values.Max() + jokers == 3)
            {
                return 3;
            }
            else if (labelCounts.Values.Count == 3)
            {
                return 4;
            }
            else if (labelCounts.Values.Count == 4)
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }

        StringReader reader = new(_input);
        List<Hand> hands = [];

        string line = reader.ReadLine();

        while (line != null)
        {
            hands.Add(new Hand(line[..5], Int32.Parse(line[6..])));

            line = reader.ReadLine();
        }

        Hand[] handsArray = [.. hands];
        Array.Sort(handsArray, (a, b) =>
        {
            int aStrength = HandStrength(a.Cards);
            int bStrength = HandStrength(b.Cards);

            int diff = bStrength - aStrength;
            if (diff != 0)
            {
                return diff;
            }

            for (int i = 0; i < a.Cards.Length; i++)
            {
                diff = Array.IndexOf(cardTypes, b.Cards[i]) - Array.IndexOf(cardTypes, a.Cards[i]);
                if (diff != 0)
                {
                    return diff;
                }
            }

            return 0;
        });

        int total = 0;

        for (int i = 0; i < handsArray.Length; i++)
        {
            total += (i + 1) * handsArray[i].Bet;
        }

        return new(total.ToString());
    }
}
