using System;
using StudentSystem.StudentSystemDb;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;
using System.Linq;
using System.Collections.Generic;

namespace StudentSystem
{
    public class Startup
    {
        public static void Main()
        {
            using (var db = new StudentSystemDbContext())
            {
                // db.Database.Migrate();
                // ClearDatabase(db);

                //SeedData(db);

                
            }
        }

        private static void SeedData(StudentSystemDbContext db)
        {
            const int totalStudents = 25;
            const int totalCourses = 10;
            var currentDate = DateTime.Now;

            //Students
            for (int i = 0; i < totalStudents; i++)
            {
                db.Students.Add(new Student
                {
                    Name = $"Student {i}",
                    RegistrationDate = currentDate.AddDays(i),
                    Birthdate = currentDate.AddYears(-20).AddDays(i),
                    Number = $"Random Phone {i}",

                });
            }

            db.SaveChanges();

            //Courses
            var addedCourses = new List<Course>();

            for (int i = 0; i < totalCourses; i++)
            {
                var course = new Course
                {
                    Name = $"Course {i}",
                    Description = $"Course Details{i}",
                    Price = (decimal)100 * i,
                    StartDate = currentDate.AddDays(i),
                    EndDate = currentDate.AddDays(20 + i),
                };

                addedCourses.Add(course);
                db.Courses.Add(course);
            }

            db.SaveChanges();

            var random = new Random();
            //Student in courses
            var studentIds = db
                      .Students
                      .Select(st => st.Id)
                      .ToList();

            for (int i = 0; i < totalCourses; i++)
            {
                var currentCourse = addedCourses[i];
                var studentsInCourse = random.Next(2, totalStudents / 2);

                for (int j = 0; j < studentsInCourse; j++)
                {
                    var studentId = studentIds[random.Next(0, studentIds.Count)];

                    if (!currentCourse.Students.Any(s => s.StudentId == studentId))
                    {
                        currentCourse.Students.Add(new StudentCourse
                        {
                            StudentId = studentId
                        });
                    }
                    else
                    {
                        j--;
                    }
                }

                var resourcesInCourse = random.Next(2, 20);
                var types = new[] { 0, 1, 2, 999 };

                currentCourse.Resources.Add(new Resource
                {
                    Name=$"Resource {i}",
                    Url=$"URL {i}",
                    Type=(ResourceType)types[random.Next(0,types.Length)]
                });
            }

            db.SaveChanges();

            //Homeworks
            for (int i = 0; i < totalCourses; i++)
            {
                var currentCourse = addedCourses[i];

                var studentsInCourseIds = currentCourse
                    .Students
                    .Select(st => st.StudentId)
                    .ToList();

                for (int j = 0; j < studentsInCourseIds.Count; j++)
                {
                    var totalHomewors = random.Next(2, 5);

                    for (int k = 0; k < totalHomewors; k++)
                    {
                        db.Homeworks.Add(new Homework
                        {
                            Content = $"Content homework {i}",
                            SubmissionDate = currentDate.AddDays(-i),
                            Type = ContentType.Zip,
                            StudentId = studentsInCourseIds[j],
                            CourseId = currentCourse.Id
                        }); 
                    }
                }
            }

            db.SaveChanges();
        }

        private static void ClearDatabase(StudentSystemDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
