using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BR.Models.Mapping
{
    public class RATE_HISTORYMap : EntityTypeConfiguration<RATE_HISTORY>
    {
        public RATE_HISTORYMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.GroupCode)
                .HasMaxLength(50);

            this.Property(t => t.BankCode)
                .HasMaxLength(50);

            this.Property(t => t.TenorCode)
                .HasMaxLength(50);

            this.Property(t => t.InsRate)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("RATE_HISTORY");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.GroupCode).HasColumnName("GroupCode");
            this.Property(t => t.BankCode).HasColumnName("BankCode");
            this.Property(t => t.TenorCode).HasColumnName("TenorCode");
            this.Property(t => t.InsRate).HasColumnName("InsRate");
            this.Property(t => t.Timestamp).HasColumnName("Timestamp");
        }
    }
}
