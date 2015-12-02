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
        public IStudent Student { get; private set; }

        public Grade(double weight, double value, IStudent student)
        {
            this.Weight = weight;
            this.Value = value;
            this.Student = student;
        }
    }
}
