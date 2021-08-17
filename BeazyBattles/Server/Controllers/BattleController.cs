using BeazyBattles.Server.Data;
using BeazyBattles.Server.Services;
using BeazyBattles.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeazyBattles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BattleController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public BattleController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<IActionResult> StartBattle([FromBody] int opponentId)
        {
            var attacker = await _utilityService.GetUser();
            var opponent = await _context.Users.FindAsync(opponentId);
            if(opponent == null || opponent.IsDeleted){
                return NotFound("Opponent is not available.");
            }

            var result = new BattleResult();
            await Fight(attacker, opponent, result);

            return Ok(result);
        }

        private async Task Fight(User attacker, User opponent, BattleResult result)
        {
            var attackerArmy = await _context.UserUnits
                .Where(u => u.UserId == attacker.Id && u.HitPoints > 0)
                .Include(u => u.Unit)
                .ToListAsync();
            var opponentArmy = await _context.UserUnits
                .Where(u => u.UserId == opponent.Id && u.HitPoints > 0)
                .Include(u => u.Unit)
                .ToListAsync();

            bool userAttacking;

            int currentRound = 0;
            while (attackerArmy.Count > 0 && opponentArmy.Count > 0)
            {
                currentRound++;

                if (currentRound % 2 != 0)
                {
                    userAttacking = true;
                    //attackerDamageSum += FightRound(attacker, opponent, attackerArmy, opponentArmy, userAttacking, result);
                    result = FightRound(attacker, opponent, attackerArmy, opponentArmy, userAttacking, result);
                }
                else
                { 
                    userAttacking = false;
                    //opponentDamageSum += FightRound(opponent, attacker, opponentArmy, attackerArmy, userAttacking, result);
                    result = FightRound(opponent, attacker, opponentArmy, attackerArmy, userAttacking, result);
                }
            }

            result.IsVictory = opponentArmy.Count == 0;
            result.RoundsFought = currentRound;

            //result.AttackerDamageSum = attackerDamageSum;
            //result.OpponentDamageSum = opponentDamageSum;

            if (result.RoundsFought > 0)
                await FinishFight(attacker, opponent, result);
        }

        private BattleResult FightRound(User attacker, User opponent,
            List<UserUnit> attackerArmy, List<UserUnit> opponentArmy, bool userAttacking, BattleResult result)
        {
            int randomAttackerIndex = new Random().Next(attackerArmy.Count);
            int randomOpponentIndex = new Random().Next(opponentArmy.Count);

            var randomAttacker = attackerArmy[randomAttackerIndex];
            var randomOpponent = opponentArmy[randomOpponentIndex];

            var damage =
                new Random().Next(randomAttacker.Unit.Attack) - new Random().Next(randomOpponent.Unit.Defense);
            if (damage < 0) damage = 0;

            if (damage < randomOpponent.HitPoints)
            {
                randomOpponent.HitPoints -= damage;
                result.Log.Add(
                    $"{attacker.Username}'s {randomAttacker.Unit.Title} attacks " +
                    $"{opponent.Username}'s {randomOpponent.Unit.Title} with {damage} damage.");
                if (userAttacking)
                {
                    result.AttackerDamageSum += damage;
                }
                else
                {
                    result.OpponentDamageSum += damage;
                }
                
                return result;
            }
            else
            {
                damage = randomOpponent.HitPoints;
                randomOpponent.HitPoints = 0;
                opponentArmy.Remove(randomOpponent);
                int deathToll = Convert.ToInt32(Math.Ceiling(randomOpponent.Unit.BananaCost * 0.2));
                var battleReward = _utilityService.RollReward();

                if (!userAttacking)
                {
                    result.OpponentRewardSum += Convert.ToInt32(Math.Floor(deathToll * battleReward.RewardMultiplier));
                    result.OpponentDamageSum += randomOpponent.HitPoints;
                    result.AttackerRewardSum += deathToll;
                    result.Log.Add(
                        $"{attacker.Username}'s {randomAttacker.Unit.Title} killed " +
                        $"your {randomOpponent.Unit.Title}!");
                    result.Log.Add($"You claim back the death toll of {deathToll} bananas.");
                } 
                else
                {
                    result.AttackerRewardSum += Convert.ToInt32(Math.Floor(deathToll * battleReward.RewardMultiplier));
                    result.AttackerDamageSum += randomOpponent.HitPoints;
                    result.OpponentRewardSum += deathToll;
                    result.Log.Add(
                        $"Your {randomAttacker.Unit.Title} killed " +
                        $"{opponent.Username}'s {randomOpponent.Unit.Title}!");
                    result.Log.Add($"You claim the {battleReward.RewardTitle} Reward of {deathToll * battleReward.RewardMultiplier} bananas.");
                }

                return result;
            }
        }

        private async Task FinishFight(User attacker, User opponent, BattleResult result)
        {

            attacker.Battles++;
            opponent.Battles++;

            result.Log.Add($"The fight with {opponent.Username} ended with you claiming a total of {result.AttackerRewardSum} bananas.");

            if (result.IsVictory)
            {
                attacker.Victories++;
                opponent.Defeats++;
                attacker.Bananas += result.AttackerRewardSum;
                opponent.Bananas += result.OpponentRewardSum;
                opponent.Alive = false;
                attacker.Alive = true;
            }
            else
            {
                attacker.Defeats++;
                opponent.Victories++;
                attacker.Bananas += result.AttackerRewardSum;
                opponent.Bananas += result.OpponentRewardSum;
                attacker.Alive = false;
                opponent.Alive = true;
            }

            StoreBattleHistory(attacker, opponent, result);

            await _context.SaveChangesAsync();
        }

        private void StoreBattleHistory(User attacker, User opponent, BattleResult result)
        {
            var battle = new Battle();
            battle.Attacker = attacker;
            battle.Opponent = opponent;
            battle.RoundsFought = result.RoundsFought;
            battle.AttackerWinnings = result.AttackerRewardSum;
            battle.OpponentWinnings = result.OpponentRewardSum;
            battle.Winner = result.IsVictory ? attacker : opponent;

            _context.Battles.Add(battle);
        }
    }
}
