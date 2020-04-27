using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BR.Models.Mapping
{
    public class BANK_RULESMap : EntityTypeConfiguration<BANK_RULES>
    {
        public BANK_RULESMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BankCode, t.TenorCode });

            // Properties
            this.Property(t => t.BankCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TenorCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BankRule)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("BANK_RULES");
            this.Property(t => t.BankCode).HasColumnName("BankCode");
            this.Property(t => t.TenorCode).HasColumnName("TenorCode");
            this.Property(t => t.BankRule).HasColumnName("BankRule");
        }
    }
}
