﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public interface IIdentifiable
    {
        long Id { get; set; }
        bool IsPersisted { get; }
    }
}
