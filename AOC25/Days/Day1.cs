namespace AOC25.Days;

public class Day1 : IDay
{
    private int _position = 50;
    private int _click = 0;

    public string SolvePart1(string input)
    {
        var instructions = ParseInstructions(input);
        foreach (var instruction in instructions)
        {
            for (int i = 0; i < instruction.Steps; i++)
            {
                _position += (int)instruction.Direction;
                if (_position < 0)
                {
                    _position = 99;
                }
                else if (_position > 99)
                {
                    _position = 0;
                }
            }

            if (_position == 0)
                _click++;
        }

        return _click.ToString();
    }

    public string SolvePart2(string input)
    {
        var instructions = ParseInstructions(input);
        foreach (var instruction in instructions)
        {
            for (int i = 0; i < instruction.Steps; i++)
            {
                _position += (int)instruction.Direction;
                if (_position < 0)
                {
                    _position = 99;
                }
                else if (_position > 99)
                {
                    _position = 0;
                }
                
                if (_position == 0)
                    _click++;
            }
        }

        return _click.ToString();
    }

    private List<Instruction> ParseInstructions(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var instructions = new List<Instruction>();
        foreach (var line in lines)
        {
            var direction = line[0] == 'L' ? Direction.L : Direction.R;
            var steps = int.Parse(line[1..]);
            instructions.Add(new Instruction { Direction = direction, Steps = steps });
        }
        return instructions;
    }

    private class Instruction
    {
        public Direction Direction { get; set; }
        public int Steps { get; set; }
    }

    private enum Direction
    {
        L = -1,
        R = 1
    }
}