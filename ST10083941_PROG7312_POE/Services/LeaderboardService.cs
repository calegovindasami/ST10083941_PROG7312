using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083941_PROG7312_POE.Services
{
    public static class LeaderboardService
    {
        private static List<TimeSpan> Speedruns = new();
        public static void Add(TimeSpan speedrun)
        {
            Speedruns.Add(speedrun);
        }

        //Orders the speedrun formats and adds labeling.
        public static List<string> Get()
        {
            Speedruns = Speedruns.OrderBy(x => x).ToList();
            var formattedSpeedruns = new List<string>();
            for (int i = 0; i < Speedruns.Count; i++)
            {
                formattedSpeedruns.Add($"{i + 1}) {Speedruns[i].ToString(@"hh\:mm\:ss")}");
            }
            return formattedSpeedruns;
        }
    }
}
