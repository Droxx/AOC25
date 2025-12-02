using System.Text.RegularExpressions;

namespace AOC25.Days;

public class Day2 : IDay
{
    public string SolvePart1(string input)
    {
        var ranges = ParseRanges(input);
        
        var invalidIds = new List<long>();
        
        foreach (var range in ranges)
        {
            for(long i = range.Start; i <= range.End; i++)
            {
                var id = i.ToString();
                if(id.Length % 2 != 0)
                    continue;
                var firstHalf = id.Substring(0, id.Length / 2);
                var secondHalf = id.Substring(id.Length / 2, id.Length / 2);
                if (firstHalf == secondHalf)
                {
                    invalidIds.Add(i);
                }
            }
        }

        return invalidIds.Sum().ToString();
    }

    public string SolvePart2(string input)
    {
        var ranges = ParseRanges(input);
        
        var invalidIds = new List<long>();
        
        foreach (var range in ranges)
        {
            for(long i = range.Start; i <= range.End; i++)
            {
                var id = i.ToString();
                var checks = id.Length / 2;
                for (int c = 1; c <= checks; c++)
                {
                    var sample = id.Substring(0, c);
                    var repeated = string.Concat(Enumerable.Repeat(sample, id.Length / c));
                    if (repeated == id)
                    {
                        invalidIds.Add(i);
                        break;
                    }
                }
            }
        }

        return invalidIds.Sum().ToString();
    }
    
    private List<Range> ParseRanges(string input)
    {
        var lines = input.Split(",", StringSplitOptions.RemoveEmptyEntries);
        var ranges = new List<Range>();
        foreach (var line in lines)
        {
            var parts = line.Split("-", StringSplitOptions.RemoveEmptyEntries);
            ranges.Add(new Range
            {
                Start = long.Parse(parts[0]),
                End = long.Parse(parts[1])
            });
        }

        return ranges;
    }

    private class Range
    {
        public long Start { get; set; }
        public long End { get; set; }
    }
}