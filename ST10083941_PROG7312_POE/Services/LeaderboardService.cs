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
        public static List<string> Get()
        {
            Speedruns = Speedruns.OrderByDescending(x => x).ToList();
            var formattedSpeedruns = Speedruns.Select(x => x.ToString(@"hh\:mm\:ss")).ToList();
            return formattedSpeedruns;
        }
    }
}
