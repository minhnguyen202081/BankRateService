using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BR.Models.Mapping;

namespace BR.Models
{
    public partial class BRContext : DbContext
    {
        static BRContext()
        {
            Database.SetInitializer<BRContext>(null);
        }

        public BRContext()
            : base("Name=BRContext")
        {
        }

        public DbSet<BANK_RULES> BANK_RULES { get; set; }
        public DbSet<BANK> BANKS { get; set; }
        public DbSet<ERROR_LOG> ERROR_LOG { get; set; }
        public DbSet<RATE_HISTORY> RATE_HISTORY { get; set; }
        public DbSet<TENOR> TENORS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BANK_RULESMap());
            modelBuilder.Configurations.Add(new BANKMap());
            modelBuilder.Configurations.Add(new ERROR_LOGMap());
            modelBuilder.Configurations.Add(new RATE_HISTORYMap());
            modelBuilder.Configurations.Add(new TENORMap());
        }
    }
}
