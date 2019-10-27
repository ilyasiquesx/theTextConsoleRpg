using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testConsoleGame
{
    public class Monster
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        Random Random { get; set; }
        public Position Position { get; set; }

        string[] names = { "Тролль", "Орк", "Воришка", "Саблезуб" };
        public Monster(Position p, Player player)
        {
            Random = new Random();
            Name = names[Random.Next(0, names.Length)];
            Level = Random.Next(player.Level, player.Level+3);
            Value = Level * Random.Next(40, 80);
            Position = p;
        }
    }
}

