using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeazyBattles.Shared;
using BeazyBattles.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace BeazyBattles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly DataContext _context;

        public UnitController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnits()
        {
            var units = await _context.Units.ToListAsync();
            return Ok(units);
        }
        [HttpPost]
        public async Task<IActionResult> AddUnit(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
            return Ok(await _context.Units.ToListAsync());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, Unit unit)
        {
            var dbUnit = await _context.Units.FirstOrDefaultAsync(u => u.Id == id);
            if (dbUnit == null)
            {
                return NotFound("Unit with the given Id couldn't be found.");
            }
            if (unit.Title != null)
            {
                dbUnit.Title = unit.Title;
            }
            if (unit.Attack != 0)
            {
                dbUnit.Attack = unit.Attack;
            }
            if (unit.Defense != 0)
            {
                dbUnit.Defense = unit.Defense;
            }
            if (unit.BananaCost != 0)
            {
                dbUnit.BananaCost = unit.BananaCost;
            }
            if (unit.HitPoints != 0)
            {
                dbUnit.HitPoints = unit.HitPoints;
            }
            if (unit.IconPath != null)
            {
                dbUnit.IconPath = unit.IconPath;
            }

            await _context.SaveChangesAsync();

            return Ok(dbUnit);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var dbUnit = await _context.Units.FirstOrDefaultAsync(u => u.Id == id);
            if (dbUnit == null)
            {
                return NotFound("Unit with the given Id couldn't be found.");
            }

            _context.Units.Remove(dbUnit);
            await _context.SaveChangesAsync();

            return Ok(await _context.Units.ToListAsync());
        }
    }
}
