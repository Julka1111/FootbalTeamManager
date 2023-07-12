using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootbalTeamManager.Classes
{
    internal class Team
    {
        public string Name { get; set; }    
        public List<Player> Players { get; set; } = new List<Player>();

    }
}
