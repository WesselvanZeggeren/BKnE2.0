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

        public string name;
        public int r;
        public int g;
        public int b;
        public int score;
        public int wins;

        public static Player newPlayer(string name, int r, int g, int b, int score, int wins)
        {

            Player player = new Player();
            player.name = name;
            player.r = r;
            player.g = g;
            player.b = b;
            player.score = score;
            player.wins = wins;

            return player;
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this);
        }
    }
}
