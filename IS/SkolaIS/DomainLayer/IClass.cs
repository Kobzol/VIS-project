using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public interface IClass : IIdentifiable
    {
        int FirstYear { get; }
        int FinalYear { get; }
        Room Room { get; }
    }
}
