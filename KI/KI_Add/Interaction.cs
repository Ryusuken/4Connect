using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VierGewinntInterfaceLibrary;

namespace KI
{
    public class Interaction : AInteraction
    {
        public Interaction(MovePoint Point, EPlayers Player)
            : base(Point, Player) { }
    }
}
