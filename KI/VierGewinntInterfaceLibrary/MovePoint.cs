using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntInterfaceLibrary
{
    public class MovePoint
    {
        private int pos_x = -1;
        private int pos_y = -1;

        /// <summary>
        /// Erzeugt eine neue Instanz des MovePoints
        /// </summary>
        /// <param name="X">X-Koordinate</param>
        /// <param name="Y">Y-Koordinate</param>
        public MovePoint(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public MovePoint() { }

        /// <summary>
        /// Die X-Koordinate im Spielfeld. Muss einschließlich zwischen 0 und 6 liegen
        /// </summary>
        public int X
        {
            get { return this.pos_x; }
            set
            {
                if (value >= 0 && value < 7) this.pos_x = value;
                else this.pos_x = -1;
            }
        }

        /// <summary>
        /// Die X-Koordinate im Spielfeld. Muss einschließlich zwischen 0 und 5 liegen
        /// </summary>
        public int Y
        {
            get { return this.pos_y; }
            set
            {
                if (value >= 0 && value < 6) this.pos_y = value;
                else this.pos_y = -1;
            }
        }

        public bool IsValidPoint()
        {
            return this.pos_x >= 0 && this.pos_x < 7 && this.pos_y >= 0 && this.pos_y < 6;
        }
    }
}
