using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BR.Models.Mapping
{
    public class TestMap : EntityTypeConfiguration<Test>
    {
        public TestMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.colName)
                .HasMaxLength(50);

            this.Property(t => t.colValue)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Test");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.colName).HasColumnName("colName");
            this.Property(t => t.colValue).HasColumnName("colValue");
        }
    }
}
