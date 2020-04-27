using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BR.Models.Mapping
{
    public class TENORMap : EntityTypeConfiguration<TENOR>
    {
        public TENORMap()
        {
            // Primary Key
            this.HasKey(t => t.TenorID);

            // Properties
            this.Property(t => t.TenorCode)
                .HasMaxLength(50);

            this.Property(t => t.TenorDesc)
                .HasMaxLength(200);

            this.Property(t => t.TenorIndex)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("TENORS");
            this.Property(t => t.TenorID).HasColumnName("TenorID");
            this.Property(t => t.TenorCode).HasColumnName("TenorCode");
            this.Property(t => t.TenorDesc).HasColumnName("TenorDesc");
            this.Property(t => t.TenorIndex).HasColumnName("TenorIndex");
        }
    }
}
