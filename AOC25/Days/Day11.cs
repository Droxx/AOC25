namespace AOC25.Days;

public class Day11 : IDay
{
    public string SolvePart1(string input)
    {
        var devices = ParseInput(input);
        
        var startInputs = devices.Where(d => d.Value.IsIn).Select(d => d.Value).ToList();

        long pathCount = 0;
        
        foreach (var startInput in startInputs)
        {
            pathCount += Visit(devices, new HashSet<string>(), startInput.Key);
        }
        
        return pathCount.ToString();
    }

    private long Visit(Dictionary<string, Device> devices, HashSet<string> seen, string deviceKey)
    {
        var device = devices[deviceKey];
        seen.Add(deviceKey);

        if (device.IsOut)
            return 1;

        long sum = 0;
        foreach (var connection in device.FilteredConnections.Where(c => !seen.Contains(c)))
        {
            var newSeen = new HashSet<string>(seen);
            sum += Visit(devices, newSeen, connection);
        }

        return sum;
    }

    public string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    private Dictionary<string, Device> ParseInput(string input)
    {
        var split = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var devices = new Dictionary<string, Device>();

        foreach (var line in split)
        {
            var deviceName = line.Substring(0, line.IndexOf(':'));
            
            var remaining = line.Substring(line.IndexOf(':') + 1).Trim();
            var connections = remaining.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var device = new Device
            {
                Key = deviceName,
                Connections = connections.ToList()
            };
            devices[deviceName] = device;
        }
        return devices;
    }

    private class Device
    {
        public required string Key { get; set; }
        public List<string> Connections { get; set; } = new();
        public List<string> FilteredConnections => Connections.Where(c => c != "out").ToList();
        public bool IsOut => Connections.Any(c => c == "out");
        public bool IsIn => Key == "you";

        public override bool Equals(object? obj)
        {
            var device = obj as Device;
            return device != null && device.Key == Key && device.Connections.SequenceEqual(Connections);
        }
    }
}