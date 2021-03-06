using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeazyBattles.Shared
{
    public class Unit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int HitPoints { get; set; }
        public int BananaCost { get; set; }
        public string IconPath { get; set; }
    }
}
