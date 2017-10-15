using System.Collections.Generic;

namespace EntityFrameworkDb
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<StudentCourse> Students { get; set; } = new List<StudentCourse>();
    }
}
