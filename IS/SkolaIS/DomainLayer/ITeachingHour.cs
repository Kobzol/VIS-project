using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public interface ITeachingHour : IIdentifiable
    {
        int Day { get; }
        int Order { get; }
    }
}
