namespace AOC25.Days;

public class Day4 : IDay
{
    public string SolvePart1(string input)
    {
        var floor = ParseFloorSpots(input);

        foreach (var spot in floor.Where(spot  => spot.IsOccupied))
        {
            var x = spot.X;
            var y = spot.Y;
            
            var directions = new (int dx, int dy)[]
            {
                (-1, -1), (0, -1), (1, -1),
                (-1, 0),          (1, 0),
                (-1, 1),  (0, 1),  (1, 1)
            };
            
            var neighborsOccupied = 0;
            foreach (var (dx, dy) in directions)
            {
                var nx = x + dx;
                var ny = y + dy;
                var neighbor = floor.FirstOrDefault(s => s.X == nx && s.Y == ny);
                if (neighbor != null && neighbor.IsOccupied)
                {
                    neighborsOccupied++;
                }
            }
            
            if(neighborsOccupied < 4)
                spot.IsAccessible = true;
        }

        return floor.Count(s => s.IsAccessible).ToString();
    }

    public string SolvePart2(string input)
    {
        var floor = ParseFloorSpots(input);
        var movedRolls = new List<FloorSpot>();

        var rollsMovedThisRun = new List<FloorSpot>();
        do
        {
            rollsMovedThisRun = new List<FloorSpot>();
            foreach (var spot in floor.Where(spot => spot.IsOccupied))
            {
                var x = spot.X;
                var y = spot.Y;

                var directions = new (int dx, int dy)[]
                {
                    (-1, -1), (0, -1), (1, -1),
                    (-1, 0), (1, 0),
                    (-1, 1), (0, 1), (1, 1)
                };

                var neighborsOccupied = 0;
                foreach (var (dx, dy) in directions)
                {
                    var nx = x + dx;
                    var ny = y + dy;
                    var neighbor = floor.FirstOrDefault(s => s.X == nx && s.Y == ny);
                    if (neighbor != null && neighbor.IsOccupied)
                    {
                        neighborsOccupied++;
                    }
                }

                if (neighborsOccupied < 4)
                {
                    spot.IsAccessible = true;
                    rollsMovedThisRun.Add(spot);
                }
            }

            movedRolls.AddRange(rollsMovedThisRun);
            foreach (var spot in rollsMovedThisRun)
            {
                spot.IsOccupied = false;
            }
        } while(rollsMovedThisRun.Count > 0);

        return movedRolls.Count().ToString();
    }
    
    private List<FloorSpot> ParseFloorSpots(string input)
    {
        var spots = new List<FloorSpot>();
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                var ch = line[x];
                if (ch == '.')
                {
                    spots.Add(new FloorSpot
                    {
                        X = x,
                        Y = y,
                        IsOccupied = false
                    });
                }
                else if (ch == '@')
                {
                    spots.Add(new FloorSpot
                    {
                        X = x,
                        Y = y,
                        IsOccupied = true
                    });
                }
            }
        }

        return spots;
    }

    private class FloorSpot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsAccessible { get; set; }
    }
}