using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Class : IdentifiableObject, IClass
    {
        public int FirstYear { get; private set; }
        public int FinalYear { get; private set; }
        public Room Room { get; private set; }

        public Class(int firstYear, int finalYear, Room room)
        {
            this.FirstYear = firstYear;
            this.FinalYear = finalYear;
            this.Room = room;
        }
    }
}
