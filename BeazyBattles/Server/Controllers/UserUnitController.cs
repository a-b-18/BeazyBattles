﻿using BeazyBattles.Server.Data;
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
    public class UserUnitController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserUnitController(DataContext context , IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpPost("revive")]
        public async Task<IActionResult> ReviveArmy()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits
                .Where(unit => unit.UserId == user.Id)
                .Include(unit => unit.Unit)
                .ToListAsync();

            int bananaCost = 0;
            foreach (var userUnit in userUnits)
            {
                if (userUnit.HitPoints <= 0)
                {
                    bananaCost += Convert.ToInt32(Math.Floor(userUnit.Unit.BananaCost * 0.2));
                }
            }

            if (user.Bananas < bananaCost)
            {
                return BadRequest($"Not enough bananas! You need {bananaCost} bananas to revive your army.");
            }

            bool armyStillAlive = true;
            foreach (var userUnit in userUnits)
            {
                if (userUnit.HitPoints <= 0)
                {
                    bananaCost += Convert.ToInt32(Math.Floor(userUnit.Unit.BananaCost * 0.2));
                    armyStillAlive = false;
                    userUnit.HitPoints = new Random().Next(1, userUnit.Unit.HitPoints);
                }
            }

            if (armyStillAlive)
                return BadRequest("There is no need of revival. None of your army has died.");

            user.Bananas -= bananaCost;

            await _context.SaveChangesAsync();

            return Ok($"Army revived successfully for {bananaCost} bananas!");
        }

        [HttpPost]
        public async Task<IActionResult> BuildUserUnit([FromBody] int unitId)
        {
            var unit = await _context.Units.FirstOrDefaultAsync<Unit>(u => u.Id == unitId);
            var user = await _utilityService.GetUser();
            var unitCount = await _context.UserUnits.CountAsync<UserUnit>(u => u.UserId == user.Id);

            if (user.Bananas < unit.BananaCost)
            {
                return BadRequest("Need more bananas.");
            }

            if (unitCount >= 10)
            {
                return BadRequest($"You have {unitCount} units already! Have some die fighting before building more.");
            }

            user.Bananas -= unit.BananaCost;

            var newUserUnit = new UserUnit
            {
                UnitId = unit.Id,
                UserId = user.Id,
                HitPoints = unit.HitPoints
            };

            _context.UserUnits.Add(newUserUnit);
            await _context.SaveChangesAsync();
            return Ok(newUserUnit);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserUnits()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits.Where(unit => unit.UserId == user.Id).ToListAsync();

            var response = userUnits.Select(
                unit => new UserUnitResponse
                {
                    UnitId = unit.UnitId,
                    HitPoints = unit.HitPoints
                });

            return Ok(response);
        }

        [HttpPost("bury")]
        public async Task<IActionResult> BuryArmy()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits
                .Where(unit => unit.UserId == user.Id)
                .Include(unit => unit.Unit)
                .ToListAsync();

            int bananaCost = 0;

            if (user.Bananas < bananaCost)
            {
                return BadRequest("Not enough bananas! You need 1000 bananas to revive your army.");
            }

            bool armyStillAlive = true;
            foreach (var userUnit in userUnits)
            {
                if (userUnit.HitPoints <= 0)
                {
                    armyStillAlive = false;
                    _context.UserUnits.Remove(userUnit);
                    _context.SaveChanges();
                }
            }

            if (armyStillAlive)
                return BadRequest("No burial is required. None of your army has died.");

            user.Bananas -= bananaCost;

            await _context.SaveChangesAsync();

            return Ok("You successfully buried your dead.");
        }

    }
}
