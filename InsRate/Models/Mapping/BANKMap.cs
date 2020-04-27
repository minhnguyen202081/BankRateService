using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BR.Models.Mapping
{
    public class BANKMap : EntityTypeConfiguration<BANK>
    {
        public BANKMap()
        {
            // Primary Key
            this.HasKey(t => t.BankID);

            // Properties
            this.Property(t => t.BankCode)
                .HasMaxLength(20);

            this.Property(t => t.BankName)
                .HasMaxLength(200);

            this.Property(t => t.DataExtractor)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BANKS");
            this.Property(t => t.BankID).HasColumnName("BankID");
            this.Property(t => t.BankCode).HasColumnName("BankCode");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankLink).HasColumnName("BankLink");
            this.Property(t => t.DataExtractor).HasColumnName("DataExtractor");
        }
    }
}
