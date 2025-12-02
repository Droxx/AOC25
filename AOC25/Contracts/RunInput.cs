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
    Day2 = 2,
    Day3 = 3,
    Day4 = 4,
    Day5 = 5,
    Day6 = 6,
    Day7 = 7,
    Day8 = 8,
    Day9 = 9,
    Day10 = 10,
    Day11 = 11,
    Day12 = 12,
}

public enum Part
{
    Part1 = 1,
    Part2 = 2,
}