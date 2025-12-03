using System.Text;

namespace AOC25.Days;

public class Day3 : IDay
{
    public string SolvePart1(string input)
    {
        var banks = ParseBanks(input);
        foreach (var bank in banks)
        {
            var curHiNum = 0;
            for (var i = 0; i < bank.Battery.Count-1; i++)
            {
                if(bank.Battery[i] > curHiNum)
                {
                    curHiNum = bank.Battery[i];
                    bank.PositionFirstHiNum = i;
                }
            }

            curHiNum = 0;
            for(var i = bank.PositionFirstHiNum+1; i < bank.Battery.Count; i++)
            {
                if(bank.Battery[i.Value] > curHiNum)
                {
                    curHiNum = bank.Battery[i.Value];
                    bank.PositionSecondHiNum = i;
                }
            }
        }

        return banks.Sum(b => b.Joltage ?? 0).ToString();
    }

    public string SolvePart2(string input)
    {
        var banks = ParseBanks(input);
        foreach (var bank in banks)
        {
            bank.HighNumPositions = new List<int>(new int[12]);
            var lastPos = 0;
            for (int x = 0; x < 12; x++)
            {
                var curHiNum = 0;
                for (var i = lastPos; i < bank.Battery.Count; i++)
                {
                    // if first num, we want to leave spots at the end
                    if (i+(11-x) == bank.Battery.Count)
                    {
                        if(curHiNum == 0)
                            throw new InvalidOperationException();
                        break;
                    }
                        
                    if(bank.Battery[i] > curHiNum)
                    {
                        curHiNum = bank.Battery[i];
                        lastPos = i;
                        bank.HighNumPositions[x] = i;
                    }
                }
                lastPos++;
            }
        }

        return banks.Sum(b => b.Joltage ?? 0).ToString();
    }
    
    private List<Bank> ParseBanks(string input)
    {
        var banks = new List<Bank>();
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var bank = new Bank();
            foreach (var character in line)
            {
                bank.Battery.Add(int.Parse(character.ToString()));
            }
            banks.Add(bank);
        }
        return banks;
    }

    private class Bank
    {
        public List<int> Battery { get; set; } = new List<int>();
        public List<int> HighNumPositions { get; set; } = new List<int>();
        public int? PositionFirstHiNum { get; set; }
        public int? PositionSecondHiNum { get; set; }

        public long? Joltage
        {
            get
            {
                if (HighNumPositions.Count > 0)
                {
                    var str = new StringBuilder();
                    foreach (var position in HighNumPositions)
                    {
                        str.Append(Battery[position]);
                    }
                    return long.Parse(str.ToString());
                }
                
                if(PositionFirstHiNum.HasValue && PositionSecondHiNum.HasValue)
                {
                    return long.Parse($"{Battery[PositionFirstHiNum.Value]}{Battery[PositionSecondHiNum.Value]}");
                }

                return null;
            }
        }
    }
}