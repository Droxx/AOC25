namespace AOC25.Days;

public class Day7 : IDay
{
    public string SolvePart1(string input)
    {
        var grid = ParseGrid(input);
        int height = grid.GetLength(0);
        int width = grid.GetLength(1);
        
        var splitCount = 0;
        
        for(var r = 1; r < height; r++)
        {
            var rA = r - 1;
            for(var c = 0; c < width; c++)
            {
                if (grid[rA, c] == CellState.Source || grid[rA, c] == CellState.Beam)
                {
                    if (grid[r, c] == CellState.Empty)
                    {
                        grid[r, c] = CellState.Beam;
                    }
                    else if (grid[r, c] == CellState.Splitter)
                    {
                        splitCount++;
                        // Splitter creates beams to left and right
                        if (c > 0 && grid[r, c - 1] == CellState.Empty)
                        {
                            grid[r, c - 1] = CellState.Beam;
                        }
                        if (c < width - 1 && grid[r, c + 1] == CellState.Empty)
                        {
                            grid[r, c + 1] = CellState.Beam;
                        }
                    }
                }
            }
        }
        return splitCount.ToString();
    }

    public string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }
    
    private CellState[,] ParseGrid(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var height = lines.Length;
        var width = lines.Max(l => l.Length);
        var grid = new CellState[height, width];

        for (int y = 0; y < height; y++)
        {
            var line = lines[y];
            for (int x = 0; x < width; x++)
            {
                if (x >= line.Length)
                {
                    grid[y, x] = CellState.Empty;
                    continue;
                }

                switch (line[x])
                {
                    case 'S':
                        grid[y, x] = CellState.Source;
                        break;
                    case '^':
                        grid[y, x] = CellState.Splitter;
                        break;
                    case '|':
                        grid[y, x] = CellState.Beam;
                        break;
                    default:
                        grid[y, x] = CellState.Empty;
                        break;
                }
            }
        }

        return grid;
    }

    private class Cell
    {
        public CellState State { get; set; }
    }

    private enum CellState
    {
        Empty,
        Source,
        Splitter,
        Beam
    }
}