namespace AOC25.Contracts;

public class RunInput
{
    public Day Day { get; set; }
    public Part Part { get; set; }
    public string Input { get; set; } = string.Empty;
}

public enum Day
{
    Day1 = 1,
}

public enum Part
{
    Part1 = 1,
    Part2 = 2,
}