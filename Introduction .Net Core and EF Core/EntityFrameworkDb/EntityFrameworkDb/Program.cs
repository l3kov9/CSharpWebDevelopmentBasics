namespace EntityFrameworkDb
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            var departmentId = 1;

            using (var db = new AppDbContext())
            {
                var result = db
                    .WorkSectors
                    .Where(w => w.Id == departmentId)
                    .Select(d => new
                    {
                        d.Name,
                        Employees = d.Workers.Count
                    })
                    .FirstOrDefault();


                //var workSector = db
                //    .WorkSectors
                //    .FirstOrDefault(ws => ws.Id == departmentId);

                //if (true)
                //{
                //    db
                //        .Entry(workSector)
                //        .Collection(d => d.Workers)
                //        .Load();

                //    var queryEmp = db
                //        .Entry(workSector)
                //        .Collection(d => d.Workers)
                //        .Query()
                //        .FirstOrDefault(x => x.Name.Contains("1"));

                //    Console.WriteLine(queryEmp.Name);
                //}
            }
        }
    }
}
