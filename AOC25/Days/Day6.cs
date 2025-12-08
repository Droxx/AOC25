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
        var width = lines.Max(l=>l.Length);
        var columns = width / ColumnWidth;
        
        var problems = new Problem[columns];
        
        // For each column
        for(int c = 0; c < columns; c++)
        {
            var prob = new Problem();
            var sbs = new StringBuilder[ColumnWidth];
            
            var column = lines.Select(l => l.Substring(c * ColumnWidth, ColumnWidth)).ToArray();
            
            // for each line in that column
            for(int l = ColumnWidth-2; l >= 0; l--)
            {
                sbs[l] = new StringBuilder();
                for (var r = 0; r < lines.Length - 1; r++)
                {
                    var ch = column[r][l].ToString();
                    if(!string.IsNullOrEmpty(ch))
                        sbs[l].Append(ch);
                }
                prob.Numbers.Add(sbs[l].ToString().Replace(" ", ""));
            }
            
            var opChar = column[^1].Trim();
            prob.Operator = opChar == "+" ? Operator.Add : Operator.Multiply;

            problems[c] = prob;
        }

        return problems.ToList();
    }
    
    private class Problem
    {
        public List<string> Numbers { get; set; } = new();
        public List<long> ParsedNumbers 
        { 
            get
            {
                return Numbers.Where(n => !string.IsNullOrEmpty(n)).Select(n => long.Parse(n)).ToList();
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