using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Grade : IdentifiableObject, IGrade
    {
        public double Weight { get; private set; }
        public double Value { get; private set; }
        public IPerson Student { get; private set; }
        public ITest Test { get; private set; }

        public Grade(double weight, double value, IPerson student, ITest test)
        {
            this.Weight = weight;
            this.Value = value;
            this.Student = student;
            this.Test = test;
        }
    }
}
