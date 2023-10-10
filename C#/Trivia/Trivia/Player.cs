using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia
{
    public class Player
    {
        public string Name { get; set; }
        public int Place { get; set; } = 0;
        public int Purses { get; set; } = 0;
        public bool InPenaltyBox { get; set; } = false;


        public Player(string name)
        {
            this.Name = name;
        }
    }
}
