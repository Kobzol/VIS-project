﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface ITeachingHourRepository : IRepository<ITeachingHour>
    {
        IEnumerable<ITeachingHour> FindByScheduleId(long scheduleId);
        ITeachingHour FindByDayOrder(int day, int order);
    }
}
