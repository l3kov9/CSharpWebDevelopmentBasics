using FootballBetting.Data.FottballBettingDb;

namespace FootballBetting
{
    public class Startup
    {
        public static void Main()
        {
            using(var db=new FootballBettingDbContext())
            {
                // db.Database.Migrate();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
