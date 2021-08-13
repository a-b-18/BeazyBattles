using BeazyBattles.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeazyBattles.Client.Services
{
    public interface ILeaderboardService
    {
        IList<UserStatistic> Leaderboard { get; set; }
        Task GetLeaderboard();
    }
}
