namespace EntityFrameworkDb
{
    public class Worker
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int WorkSectorId { get; set; }

        public WorkSector WorkSector { get; set; }
    }
}
