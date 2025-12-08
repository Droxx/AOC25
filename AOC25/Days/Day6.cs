using System.Text;

namespace AOC25.Days;

public class Day6 : IDay
{
    public string SolvePart1(string input)
    {
        var problems = ParseProblems(input);
        return problems.Sum(p => p.Total).ToString();
    }

    public string SolvePart2(string input)
    {
        var problems = ParseProblemsPart2(input);
        return problems.Sum(p => p.Total).ToString();
    }
    
    private List<Problem> ParseProblems(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var problems = new List<Problem>();
        var maxProbs = lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
        for(int i = 0; i < maxProbs; i++)
        {
            var problem = new Problem();
            foreach (var line in lines)
            {
                var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (parts[i].Contains("+") || parts[i].Contains("*"))
                {
                    problem.Operator = parts[i] == "+" ? Operator.Add : Operator.Multiply;
                    continue;
                }

                problem.Numbers.Add(parts[i]);
            }
            problems.Add(problem);
        }

        return problems;
    }

    private const int ColumnWidth = 5;
    
    private List<Problem> ParseProblemsPart2(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var matrix = new string[lines.Length, lines.Max(l => l.Length)]; // y,x
        // populate the matrix
        for(int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for(var x = 0; x < line.Length; x++)
            {
                matrix[y, x] = line[x].ToString();
            }
        }
        
        var rotated = RotateMatrixCounterClockwise(matrix);
        
        var problems = new Problem[input.Count(i => i == '+' || i == '*')];
        problems[0] = new Problem();
        
        var problemCount = 0;
        for (var row = 0; row < rotated.GetLength(0); row++)
        {
            var sb = new StringBuilder();
            for(var i = 0; i < rotated.GetLength(1)-1; i++)
            {
                if(!string.IsNullOrWhiteSpace(rotated[row,i]))
                    sb.Append(rotated[row, i] ?? " ");
            }
            problems[problemCount].Numbers.Add(sb.ToString());
            
            if(rotated[row, rotated.GetLength(1)-1] == "+" || rotated[row, rotated.GetLength(1)-1] == "*")
            {
                problems[problemCount].Operator = rotated[row, rotated.GetLength(1)-1] == "+" ? Operator.Add : Operator.Multiply;
                problemCount++;
                if(problemCount < problems.Length)
                    problems[problemCount] = new Problem();
            }
        }
        

        return problems.ToList();
    }
    
    static string[,] RotateMatrixCounterClockwise(string[,] oldMatrix)
    {
        string[,] newMatrix = new string[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
        int newColumn, newRow = 0;
        for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
        {
            newColumn = 0;
            for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
            {
                newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                newColumn++;
            }
            newRow++;
        }
        return newMatrix;
    }
    
    private class Problem
    {
        public List<string> Numbers { get; set; } = new();
        public List<long> ParsedNumbers 
        { 
            get
            {
                return Numbers.Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => long.Parse(n)).ToList();
            }
        }
        public Operator Operator { get; set; }
        
        public long Total 
        {
            get
            {
                return Operator switch
                {
                    Operator.Add => ParsedNumbers.Sum(),
                    Operator.Multiply => ParsedNumbers.Aggregate(1L, (acc, val) => acc * val),
                    _ => throw new InvalidOperationException()
                };
            }
        }
    }

    private enum Operator
    {
        Add,
        Multiply
    }
}