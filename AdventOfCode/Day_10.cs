namespace AdventOfCode;

public class Day_10 : BaseDay
{
    private readonly string _input;

    public Day_10()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    private static char RecVisit(int row, int col, char[,] visited, string[] lines)
    {
        if (row < 0 || row >= lines.Length || col < 0 || col >= lines[0].Length)
        {
            return 'V';
        }

        if (visited[row, col] != '\0')
        {
            return visited[row, col];
        }

        visited[row, col] = 'V';

        int[] neighbours = [RecVisit(row + 1, col, visited, lines),
                            RecVisit(row - 1, col, visited, lines),
                            RecVisit(row, col + 1, visited, lines),
                            RecVisit(row, col - 1, visited, lines)];

        if (neighbours.Contains('A'))
        {
            return 'A';
        }
        else if (neighbours.Contains('B'))
        {
            return 'B';
        }
        else
        {
            return 'V';
        }
    }

    private static void RecMark(int row, int col, char[,] visited, string[] lines, char marking)
    {
        if (row < 0 || row >= lines.Length || col < 0 || col >= lines[0].Length)
        {
            return;
        }

        if (visited[row, col] == 'A' || visited[row, col] == 'B' || visited[row, col] == 'P')
        {
            return;
        }

        visited[row, col] = marking;

        RecMark(row + 1, col, visited, lines, marking);
        RecMark(row - 1, col, visited, lines, marking);
        RecMark(row, col + 1, visited, lines, marking);
        RecMark(row, col - 1, visited, lines, marking);
    }

    private static void Mark(int row, int col, char mark, char[,] visited, string[] lines)
    {
        if (row < 0 || row >= lines.Length || col < 0 || col >= lines[0].Length)
        {
            return;
        }
        if (visited[row, col] != 'P')
        {
            visited[row, col] = mark;
        }
    }

    public override ValueTask<string> Solve_1()
    {
        static Tuple<char, int, int> NextMove(char direction, char symbol)
        {
            switch (direction)
            {
                case 'N':
                    switch (symbol)
                    {
                        case '|':
                            return new Tuple<char, int, int>('N', 0, 1);
                        case 'L':
                            return new Tuple<char, int, int>('W', 1, 0);
                        case 'J':
                            return new Tuple<char, int, int>('E', -1, 0);
                    }
                    break;
                case 'S':
                    switch (symbol)
                    {
                        case '|':
                            return new Tuple<char, int, int>('S', 0, -1);
                        case '7':
                            return new Tuple<char, int, int>('E', -1, 0);
                        case 'F':
                            return new Tuple<char, int, int>('W', 1, 0);
                    }
                    break;
                case 'E':
                    switch (symbol)
                    {
                        case '-':
                            return new Tuple<char, int, int>('E', -1, 0);
                        case 'L':
                            return new Tuple<char, int, int>('S', 0, -1);
                        case 'F':
                            return new Tuple<char, int, int>('N', 0, 1);
                    }
                    break;
                case 'W':
                    switch (symbol)
                    {
                        case '-':
                            return new Tuple<char, int, int>('W', 1, 0);
                        case 'J':
                            return new Tuple<char, int, int>('S', 0, -1);
                        case '7':
                            return new Tuple<char, int, int>('N', 0, 1);
                    }
                    break;
            }

            return null;
        }

        string[] lines = _input.Split(Environment.NewLine);

        int startRow = 0;
        int startCol = 0;

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[0].Length; col++)
            {
                if (lines[row][col] == 'S')
                {
                    startRow = row;
                    startCol = col;
                    break;
                }
            }
        }

        int currentRow = startRow;
        int currentCol = startCol;

        char direction;
        int moves = 1;

        // This is disgusting 
        Tuple<char, int, int> tmp = null;
        if (NextMove('W', lines[currentRow][currentCol + 1]) != null)
        {
            tmp = NextMove('W', lines[currentRow][currentCol + 1]);
        }
        else if (NextMove('E', lines[currentRow][currentCol - 1]) != null)
        {
            tmp = NextMove('E', lines[currentRow][currentCol - 1]);
        }
        else if (NextMove('N', lines[currentRow + 1][currentCol]) != null)
        {
            tmp = NextMove('N', lines[currentRow + 1][currentCol]);
        }
        else if (NextMove('S', lines[currentRow - 1][currentCol]) != null)
        {
            tmp = NextMove('S', lines[currentRow - 1][currentCol]);
        }

        direction = tmp.Item1;
        currentRow += tmp.Item3;
        currentCol += tmp.Item2;

        while (currentRow != startRow || currentCol != startCol)
        {
            tmp = NextMove(direction, lines[currentRow][currentCol]);
            direction = tmp.Item1;
            currentRow += tmp.Item3;
            currentCol += tmp.Item2;

            moves += 1;
        }

        return new((moves / 2).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        static Tuple<char, int, int> NextMove(char direction, char symbol, int row, int col, char[,] visited, string[] lines)
        {
            Mark(row, col, 'P', visited, lines);
            switch (direction)
            {
                case 'N':
                    switch (symbol)
                    {
                        case '|':
                            Mark(row, col - 1, 'A', visited, lines);
                            Mark(row, col + 1, 'B', visited, lines);
                            return new Tuple<char, int, int>('N', 0, 1);
                        case 'L':
                            Mark(row, col - 1, 'A', visited, lines);
                            Mark(row + 1, col, 'A', visited, lines);
                            return new Tuple<char, int, int>('W', 1, 0);
                        case 'J':
                            Mark(row, col + 1, 'B', visited, lines);
                            Mark(row + 1, col, 'B', visited, lines);
                            return new Tuple<char, int, int>('E', -1, 0);
                    }
                    break;
                case 'S':
                    switch (symbol)
                    {
                        case '|':
                            Mark(row, col - 1, 'B', visited, lines);
                            Mark(row, col + 1, 'A', visited, lines);
                            return new Tuple<char, int, int>('S', 0, -1);
                        case '7':
                            Mark(row, col + 1, 'A', visited, lines);
                            Mark(row - 1, col, 'A', visited, lines);
                            return new Tuple<char, int, int>('E', -1, 0);
                        case 'F':
                            Mark(row, col - 1, 'B', visited, lines);
                            Mark(row - 1, col, 'B', visited, lines);
                            return new Tuple<char, int, int>('W', 1, 0);
                    }
                    break;
                case 'E':
                    switch (symbol)
                    {
                        case '-':
                            Mark(row - 1, col, 'A', visited, lines);
                            Mark(row + 1, col, 'B', visited, lines);
                            return new Tuple<char, int, int>('E', -1, 0);
                        case 'L':
                            Mark(row + 1, col, 'B', visited, lines);
                            Mark(row, col - 1, 'B', visited, lines);
                            return new Tuple<char, int, int>('S', 0, -1);
                        case 'F':
                            Mark(row - 1, col, 'A', visited, lines);
                            Mark(row, col - 1, 'A', visited, lines);
                            return new Tuple<char, int, int>('N', 0, 1);
                    }
                    break;
                case 'W':
                    switch (symbol)
                    {
                        case '-':
                            Mark(row - 1, col, 'B', visited, lines);
                            Mark(row + 1, col, 'A', visited, lines);
                            return new Tuple<char, int, int>('W', 1, 0);
                        case 'J':
                            Mark(row + 1, col, 'A', visited, lines);
                            Mark(row, col + 1, 'A', visited, lines);
                            return new Tuple<char, int, int>('S', 0, -1);
                        case '7':
                            Mark(row - 1, col, 'B', visited, lines);
                            Mark(row, col + 1, 'B', visited, lines);
                            return new Tuple<char, int, int>('N', 0, 1);
                    }
                    break;
            }

            Mark(row, col, '\0', visited, lines);
            return null;
        }

        string[] lines = _input.Split(Environment.NewLine);

        char[,] visited = new char[lines.Length, lines[0].Length];

        int startRow = 0;
        int startCol = 0;

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[0].Length; col++)
            {
                if (lines[row][col] == 'S')
                {
                    startRow = row;
                    startCol = col;
                    break;
                }
            }
        }

        int currentRow = startRow;
        int currentCol = startCol;

        visited[startRow, startCol] = 'P';

        char direction;

        // This is disgusting 
        Tuple<char, int, int> tmp = null;
        if (NextMove('W', lines[currentRow][currentCol + 1], currentRow, currentCol, visited, lines) != null)
        {
            tmp = NextMove('W', lines[currentRow][currentCol + 1], currentRow, currentCol, visited, lines);
        }
        else if (NextMove('E', lines[currentRow][currentCol - 1], currentRow, currentCol, visited, lines) != null)
        {
            tmp = NextMove('E', lines[currentRow][currentCol - 1], currentRow, currentCol, visited, lines);
        }
        else if (NextMove('N', lines[currentRow + 1][currentCol], currentRow, currentCol, visited, lines) != null)
        {
            tmp = NextMove('N', lines[currentRow + 1][currentCol], currentRow, currentCol, visited, lines);
        }
        else if (NextMove('S', lines[currentRow - 1][currentCol], currentRow, currentCol, visited, lines) != null)
        {
            tmp = NextMove('S', lines[currentRow - 1][currentCol], currentRow, currentCol, visited, lines);
        }

        direction = tmp.Item1;
        currentRow += tmp.Item3;
        currentCol += tmp.Item2;

        while (currentRow != startRow || currentCol != startCol)
        {
            visited[currentRow, currentCol] = 'P';

            tmp = NextMove(direction, lines[currentRow][currentCol], currentRow, currentCol, visited, lines);
            direction = tmp.Item1;
            currentRow += tmp.Item3;
            currentCol += tmp.Item2;
        }

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[0].Length; col++)
            {
                if (visited[row, col] == 'A' || visited[row, col] == 'B' || visited[row, col] == 'P')
                {
                    continue;
                }

                char marking = RecVisit(row, col, visited, lines);
                RecMark(row, col, visited, lines, marking);
            }
        }

        char outside = 'P';
        for (int row = 0; row < visited.GetLength(0); row++)
        {
            if (visited[row, 0] != 'P')
            {
                outside = visited[row, 0];
                break;
            }
            else if (visited[row, visited.GetLength(1) - 1] != 'P')
            {
                outside = visited[row, visited.GetLength(1) - 1];
                break;
            }
        }

        for (int col = 0; col < visited.GetLength(1); col++)
        {
            if (visited[0, col] != 'P')
            {
                outside = visited[0, col];
                break;
            }
            else if (visited[visited.GetLength(0) - 1, col] != 'P')
            {
                outside = visited[visited.GetLength(0) - 1, col];
                break;
            }
        }

        char inside = outside == 'B' ? 'A' : 'B';
        int total = 0;

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[0].Length; col++)
            {
                if (visited[row, col] == inside)
                {
                    total += 1;
                }
            }
        }

        return new(total.ToString());
    }
}
