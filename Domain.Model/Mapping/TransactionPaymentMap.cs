using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Mapping
{
    public class TransactionPaymentMap : EntityTypeConfiguration<TransactionPayment>
    {
        public TransactionPaymentMap()
        {
            //this.HasKey(k => k.TransactionId);
            this.Property(p => p.Amount).IsRequired();
            this.Property(p => p.TransactionDate).IsRequired();         
            this.Property(p => p.Reference).IsRequired().HasMaxLength(50);
            this.Property(p => p.OpeningAmount).IsRequired();
            this.Property(p => p.DueDate).IsRequired();

            this.Property(p => p.CreatorId).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.UpdatorId).IsOptional();
            this.Property(p => p.DateUpdated).IsOptional();
            this.ToTable("Transactions");
        }
    }
}
