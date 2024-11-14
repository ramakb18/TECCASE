using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECCASE.Models
{
    public class Teacher : Person
    {
        public int Id { get; }
        public List<Course> Courses { get; } = new List<Course>();

        public Teacher(int id, string firstName, string lastName) : base(firstName, lastName)
        {
            Id = id;
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
    }

}
