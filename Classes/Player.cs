using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootbalTeamManager.Classes
{
    internal class Player
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string PlayerNumber { get; set; }

        public bool IsCaptain { get; set; }
    }
}
