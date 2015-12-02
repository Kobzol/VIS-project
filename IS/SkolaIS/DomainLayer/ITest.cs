﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public interface ITest : IIdentifiable
    {
        string Name { get; }
        DateTime Date { get; }
        ISubject Subject { get; }
    }
}
