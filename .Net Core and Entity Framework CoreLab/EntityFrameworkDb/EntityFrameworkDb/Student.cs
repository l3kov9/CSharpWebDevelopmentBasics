using System.Collections.Generic;

namespace EntityFrameworkDb
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();
    }
}
