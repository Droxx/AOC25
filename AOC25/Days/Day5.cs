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
        
        var sortedByStart = parsedInput.FreshRanges.OrderBy(r => r.Start).ToList();

        var mergedRanges = new List<IngredientRange>();
        do
        {        
            var currentStart = sortedByStart[0].Start;
            var currentMax = sortedByStart[0].End;

            for(var i = 0; i < sortedByStart.Count; i++)
            {
                if (currentMax < sortedByStart[i].End)
                {
                    currentMax = sortedByStart[i].End;
                }
                if (i == sortedByStart.Count - 1)
                {
                    mergedRanges.Add(new IngredientRange
                    {
                        Start = currentStart,
                        End = currentMax
                    });
                    break;
                }
                
                var next = sortedByStart[i+1];
                if (next.Start > currentMax)
                {
                    // we hit a gap
                    mergedRanges.Add(new IngredientRange
                    {
                        Start = currentStart,
                        End = currentMax
                    });
                    
                    sortedByStart = sortedByStart.Where(s => s.Start > currentMax).ToList();
                    
                    break;
                }
            }
        } while(mergedRanges.Max(r => r.End) < parsedInput.FreshRanges.Max(r => r.End));
        
        long total = 0;
        
        foreach (var range in mergedRanges)
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