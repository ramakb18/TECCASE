using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECCASE.Models
{
    public class Student : Person
    {
        public DateTime BirthDate { get; }

        public Student(string firstName, string lastName, DateTime birthDate) : base(firstName, lastName)
        {
            BirthDate = birthDate;
        }

        public int Age => Utilities.AgeCalculator.CalculateAge(BirthDate);
    }
}
