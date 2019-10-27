using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testConsoleGame
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public override bool Equals(object o)
        {
            if (o != null)
            {
                Position p = o as Position;
                if (p != null)
                {
                    return p.X == X && p.Y == Y;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
