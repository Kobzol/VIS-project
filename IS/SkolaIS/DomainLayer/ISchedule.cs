﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public interface ISchedule : IIdentifiable
    {
        IEnumerable<ITeachingHour> Hours { get; }
    }
}
