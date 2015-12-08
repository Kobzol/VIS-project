using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public interface ISupplement : IIdentifiable
    {
        bool IsHourCanceled { get; }
        DateTime Date { get; }
        ITeachingHour Hour { get; }
        ISchedule Schedule { get; }
        IPerson Teacher { get; }
    }
}
