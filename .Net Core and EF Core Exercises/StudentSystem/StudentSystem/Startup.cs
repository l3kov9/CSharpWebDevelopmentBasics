namespace StudentSystem
{
    using System;
    using StudentSystem.StudentSystemDb;
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Models;
    using System.Linq;
    using System.Collections.Generic;

    public class Startup
    {
        public static void Main()
        {
            using (var db = new StudentSystemDbContext())
            {
                // db.Database.Migrate();
                // ClearDatabase(db);

                // SeedData(db);
                // SeedLicences(db);

                Print(db);
            }
        }

        private static void SeedLicences(StudentSystemDbContext db)
        {
            var random = new Random();

            var resourseIds = db
                .Resources
                .Select(r => r.Id)
                .ToList();

            for (int i = 0; i < resourseIds.Count; i++)
            {
                var totalLicesces = random.Next(1, 4);

                for (int j = 0; j < totalLicesces; j++)
                {
                    db.Licenses.Add(new License
                    {
                        Name = $"License {i} {j}",
                        ResourceId = resourseIds[i]
                    });
                }
            }

            db.SaveChanges();
        }

        private static void Print(StudentSystemDbContext db) =>
            // PrintAllStudentAndHomeworks(db);

            // PrintAllCoursesAndResources(db);

            // PrintCoursesWithMoreThan5Resources(db);

            // PrintCourseActionOnADate(db);

            // PrintStudentsInfo(db);

            //PrintAllCoursesAndResourcesWithLicenses(db);

            PrintStudentInformation(db);

        private static void PrintStudentInformation(StudentSystemDbContext db)
        {
            var result = db
                .Students
                .Where(s => s.Courses.Any())
                .Select(s => new
                {
                    s.Name,
                    Courses = s.Courses.Count,
                    Resources = s.Courses.Sum(c => c.Course.Resources.Count),
                    Licenses = s.Courses.Sum(c => c.Course.Resources.Sum(r => r.Licenses.Count))
                })
                .OrderByDescending(s => s.Courses)
                .ThenByDescending(s => s.Resources)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var student in result)
            {
                Console.WriteLine($"Name: {student.Name}, courses: {student.Courses}, resources: {student.Resources}, licenses: {student.Licenses}");
            }
        }

        private static void PrintAllCoursesAndResourcesWithLicenses(StudentSystemDbContext db)
        {
            var result = db
                .Courses
                .OrderByDescending(c => c.Resources.Count)
                .ThenBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    Resources = c
                    .Resources
                    .OrderByDescending(r => r.Licenses.Count)
                    .ThenBy(r => r.Name)
                    .Select(r => new
                    {
                        r.Name,
                        Licences = r.Licenses.Select(l => l.Name),
                    })
                })
                .ToList();

            foreach (var course in result)
            {
                Console.WriteLine($"Course name: {course.Name}");
                foreach (var resource in course.Resources)
                {
                    Console.WriteLine($"Resource name: {resource.Name}");
                    Console.WriteLine($"Licenses: {string.Join(" ,", resource.Licences)}");
                }

                Console.WriteLine(new string('-', 66));
            }
        }

        private static void PrintStudentsInfo(StudentSystemDbContext db)
        {
            var studentsData = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    Courses = s.Courses.Count,
                    TotalPrice = s.Courses.Sum(c => c.Course.Price),
                    AveragePrice = s.Courses.Average(c => c.Course.Price)
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.Courses)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var student in studentsData)
            {
                Console.WriteLine($"{student.Name} has {student.Courses}, total Price: {student.TotalPrice:f2}, average price {student.AveragePrice:f2}");
            }
        }

        private static void PrintCourseActionOnADate(StudentSystemDbContext db)
        {
            var date = DateTime.Now.AddDays(25);

            var result = db
                .Courses
                .Where(c => c.StartDate < date && date < c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    Duration = c.StartDate.Subtract(c.EndDate),
                    Students = c.Students.Count
                })
                .OrderByDescending(c => c.Students)
                .ThenByDescending(c => c.Duration)
                .ToList();

            foreach (var course in result)
            {
                Console.WriteLine($"{course.Name} - Starts: {course.StartDate:dd:MM:yyyy}, Ends: {course.EndDate:dd:MM:yyyy}"
                    + $", Duration: {course.Duration.TotalDays}, Students: {course.Students}");
            }
        }

        private static void PrintCoursesWithMoreThan5Resources(StudentSystemDbContext db)
        {
            db
               .Courses
               .Where(c => c.Resources.Count > 5)
               .OrderByDescending(c => c.Resources.Count)
               .ThenByDescending(c => c.StartDate)
               .Select(c => new
               {
                   c.Name,
                   Resources = c.Resources.Count
               })
               .ToList()
               .ForEach(c => Console.WriteLine($"Course name: {c.Name}, resource count: {c.Resources}"));
        }

        private static void PrintAllCoursesAndResources(StudentSystemDbContext db)
        {
            var courses = db
                .Courses
                .OrderBy(c => c.StartDate)
                .ThenBy(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Resources = c.Resources.Select(r => new
                    {
                        r.Name,
                        r.Type,
                        r.Url,
                        RescourceCount = c.Resources.Count
                    })
                })
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course name: {course.Name}");
                foreach (var resource in course.Resources)
                {
                    Console.WriteLine($"Resource count: {resource.RescourceCount}");
                }
            }
        }

        private static void PrintAllStudentAndHomeworks(StudentSystemDbContext db)
        {
            var students = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    Homeworks = s.Homeworks.Select(h => new
                    {
                        h.Content,
                        h.Type
                    })
                })
                .ToList();

            foreach (var student in students)
            {
                var name = student.Name;
                var content = string.Empty;
                ContentType contentType = ContentType.Application;

                foreach (var homework in student.Homeworks)
                {
                    content = homework.Content;
                    contentType = homework.Type;
                }

                Console.WriteLine($"Name: {name}. Type: {contentType}, content: {content}");
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

                for (int j = 0; j < resourcesInCourse; j++)
                {
                    currentCourse.Resources.Add(new Resource
                    {
                        Name = $"Resource {i} {j}",
                        Url = $"URL {i} {j}",
                        Type = (ResourceType)types[random.Next(0, types.Length)]
                    });
                }
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
