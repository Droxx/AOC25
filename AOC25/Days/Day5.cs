namespace AOC25.Days;

public class Day5 : IDay
{
    public string SolvePart1(string input)
    {
        var parsedInput = ParseInput(input);
        var freshIngredients = new List<long>();

        foreach (var ingredient in parsedInput.AvailableIngredients)
        {
            if(parsedInput.FreshRanges.Any(r => r.IsInRange(ingredient)))
                freshIngredients.Add(ingredient);
        }
        
        return freshIngredients.Count().ToString();
    }

    public string SolvePart2(string input)
    {
        var parsedInput = ParseInput(input);
        var finalRanges = new List<IngredientRange>();
        var max = parsedInput.FreshRanges.Max(r => r.End);
        var min = parsedInput.FreshRanges.Min(r => r.Start);

        bool rangesHit = false;
        
        do
        {
            var nextEarliestStart = parsedInput.FreshRanges.Where(f => f.Start > (finalRanges.Any() ? finalRanges.Max(r => r.Start) : 0)).Min(r => r.Start);
            var newRange = parsedInput.FreshRanges.First(f => f.Start == nextEarliestStart);

            if (newRange.End < (finalRanges.Any() ? finalRanges.Max(r => r.End) : 0))
            {
                parsedInput.FreshRanges.Remove(newRange);
            }

            if (finalRanges.Any() && newRange.Start < finalRanges.Max(r => r.End))
            {
                var finalRangeToExtend = finalRanges.First(f => f.End >= newRange.Start);
                finalRangeToExtend.End = newRange.End;
                parsedInput.FreshRanges.Remove(newRange);
            }
            else
            {
                finalRanges.Add(new IngredientRange()
                {
                    Start = newRange.Start,
                    End = newRange.End
                });
            }
            
            if(finalRanges.Min(f => f.Start) == min &&
               finalRanges.Max(f => f.End) == max)
            {
                rangesHit = true;
            }
            
        } while (!rangesHit);

        long total = 0;
        
        foreach (var range in finalRanges)
        {
            total += (range.End - range.Start + 1);
        }
        return total.ToString();
    }

    private Input ParseInput(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var freshRanges = new List<IngredientRange>();
        var availableIngredients = new List<long>();
        foreach (var line in lines)
        {
            if (line.Contains('-'))
            {
                var parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
                freshRanges.Add(new IngredientRange
                {
                    Start = long.Parse(parts[0]),
                    End = long.Parse(parts[1])
                });
            }
            else
            {
                availableIngredients.Add(long.Parse(line));
            }
        }

        return new Input
        {
            FreshRanges = freshRanges,
            AvailableIngredients = availableIngredients
        };
    }
    
    private class Input
    {
        public List<IngredientRange> FreshRanges { get; set; }
        public List<long> AvailableIngredients { get; set; }
    }

    private class IngredientRange
    {
        public long Start { get; set; }
        public long End { get; set; }
        
        public bool IsInRange(long ingredient)
        {
            return ingredient >= Start && ingredient <= End;
        }
    }
}