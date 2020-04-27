using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BR.Models.Mapping
{
    public class ERROR_LOGMap : EntityTypeConfiguration<ERROR_LOG>
    {
        public ERROR_LOGMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ERROR_LOG");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Timestamp).HasColumnName("Timestamp");
            this.Property(t => t.ErrorMessage).HasColumnName("ErrorMessage");
            this.Property(t => t.StackTrace).HasColumnName("StackTrace");
            this.Property(t => t.InnerException).HasColumnName("InnerException");
        }
    }
}
