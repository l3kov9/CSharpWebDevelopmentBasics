using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Number { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? Birthdate { get; set; }

        public List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

        public List<Homework> Homeworks { get; set; } = new List<Homework>();
    }
}
