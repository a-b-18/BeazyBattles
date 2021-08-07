using BeazyBattles.Shared;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeazyBattles.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toastService;

        public UnitService(IToastService toastService)
        {
            _toastService = toastService;
        }

        public IList<Unit> Units => new List<Unit>
        {
            new Unit { Id = 1, Title = "Knight", Attack = 10, Defense = 10, BananaCost = 100, IconPath = "icons/knight.png"},
            new Unit { Id = 2, Title = "Archer", Attack = 15, Defense = 5, BananaCost = 150, IconPath = "icons/archer.png"},
            new Unit { Id = 3, Title = "Mage", Attack = 20, Defense = 1, BananaCost = 200, IconPath = "icons/mage.png"}
        };
        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();

        public void AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
            MyUnits.Add(new UserUnit { UnitId = unit.Id, HitPoints = unit.HitPoints });
            _toastService.ShowSuccess($"Your purchase of {unit.Title} was successful.", "Unit Built");
        }
    }
}
