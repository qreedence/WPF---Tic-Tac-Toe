using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppEden
{
    public static class GameState
    {
        public static string PlayerName { get; set; } = "";
        public static int PlayerScore { get; set; }
        public static int CpuScore { get; set; }

        public static List<List<string>> WinningCombinations = new List<List<string>>
        {
            new List<string> { "A1", "A2", "A3" },
            new List<string> { "B1", "B2", "B3" },
            new List<string> { "C1", "C2", "C3" },
            new List<string> { "A1", "B1", "C1" },
            new List<string> { "A2", "B2", "C2" },
            new List<string> { "A3", "B3", "C3" },
            new List<string> { "A1", "B2", "C3" },
            new List<string> { "A3", "B2", "C1" }
        };
    }
    public enum WinState
    {
        InSession = 0,
        PlayerWins = 1,
        CpuWins = 2,
        Draw = 3,
    }

    public enum TurnEnder
    {
        Player = 0,
        CPU = 1,
    }
}
