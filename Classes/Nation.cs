using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootbalTeamManager.Classes
{
    internal class Nation
    {
        public string Name { get; set; }
        public List<Player> Players { get; set; }

        //public Nation(string name, List<Player> players)
        //{
        //    Name = name;
        //    Players = players;
        //}
    }
}
