﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IPersonRepository : IRepository<IPerson>
    {
        IEnumerable<IPerson> FindBySubject(long subjectId);
        IEnumerable<IPerson> FindByRole(PersonRole personRole);
    }
}
