﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DomainLayer;

namespace ServiceLayer
{
    public class SchoolService : ISchoolService
    {
        public Person GetPerson()
        {
            return new Person(1);
        }
    }
}