using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public interface IStudent : IPerson, IIdentifiable
    {
        IClass Class { get; }
        IEnumerable<ISubject> subjects { get; }
    }
}
