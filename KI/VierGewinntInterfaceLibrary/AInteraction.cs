using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntInterfaceLibrary
{
    public abstract class AInteraction
    {
        public MovePoint Point { get; private set; }
        public EPlayers Player { get; private set; }

        public AInteraction(MovePoint Point, EPlayers Player)
        {
            this.Point = Point;
            this.Player = Player;
        }
    }
}
