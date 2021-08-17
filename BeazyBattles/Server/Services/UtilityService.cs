using BeazyBattles.Server.Data;
using BeazyBattles.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeazyBattles.Server.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IList<BattleReward> BattleRewards { get; set; } = new List<BattleReward>();

        public UtilityService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<User> GetUser()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
        public BattleReward RollReward()
        {
            BattleReward battleReward;

            // Roll between 0 and 100
            int rollValue = new Random().Next(101);

            if (rollValue < 50)
            {
                battleReward = new BattleReward
                {
                    RewardTitle = "Common",
                    RewardChance = 50,
                    RewardMultiplier = 2
                };
            }
            else if (rollValue < 80)
            {
                battleReward = new BattleReward
                {
                    RewardTitle = "Uncommon",
                    RewardChance = 30,
                    RewardMultiplier = 3.5F
                };
            }
            else if (rollValue < 95)
            {
                battleReward = new BattleReward
                {
                    RewardTitle = "Rare",
                    RewardChance = 15,
                    RewardMultiplier = 5
                };
            }
            else if (rollValue < 99)
            {
                battleReward = new BattleReward
                {
                    RewardTitle = "Epic",
                    RewardChance = 4,
                    RewardMultiplier = 10
                };
            }
            else
            {
                battleReward = new BattleReward
                {
                    RewardTitle = "Legendary",
                    RewardChance = 1,
                    RewardMultiplier = 50
                };
            }
            return battleReward;
        }
    }
}
