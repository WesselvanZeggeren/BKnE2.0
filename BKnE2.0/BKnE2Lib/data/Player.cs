using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Lib.data
{
    public class Player
    {

        public string name { get; set; }
        public int score { get; set; } 
        public int wins { get; set; }
        public Color color { get; set; }

        public static Player newPlayer(string name, int score, int wins, Color color)
        {

            Player player = new Player();
            player.name = name;
            player.score = score;
            player.wins = wins;
            player.color = color;

            return player;
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this);
        }
    }
}
