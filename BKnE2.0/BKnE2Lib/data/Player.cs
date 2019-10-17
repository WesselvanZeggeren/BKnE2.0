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
        public int plays { get; set; }
        public Color color { get; set; }
        public List<Pin> pins { get; set; }

        public static Player newPlayer(string name)
        {

            Player player = new Player();
            player.name = name;
            player.score = Config.startScore;
            player.wins = 0;
            player.color = Color.generateColor();
            player.pins = new List<Pin>();

            return player;
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this);
        }
    }
}
