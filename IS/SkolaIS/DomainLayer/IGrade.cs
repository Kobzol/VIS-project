using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public interface IGrade : IIdentifiable
    {
        double Weight { get; }
        double Value { get; }
        IStudent Student { get; }
        ITest Test { get; }
    }
}
