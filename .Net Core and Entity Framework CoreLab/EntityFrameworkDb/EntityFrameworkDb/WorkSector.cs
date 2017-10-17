using System.Collections.Generic;

namespace EntityFrameworkDb
{
    public class WorkSector
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Worker> Workers { get; set; } = new List<Worker>();
    }
}
