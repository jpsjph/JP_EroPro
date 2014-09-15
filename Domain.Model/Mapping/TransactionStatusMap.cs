using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Domain.Model.Mapping
{
    public class TransactionStatusMap: EntityTypeConfiguration<TransactionStatus>
    {
        public TransactionStatusMap()
        {
            this.HasKey(k => k.StatusId);
            this.Property(p => p.CreatorId).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.UpdatorId).IsOptional();
            this.Property(p => p.DateUpdated).IsOptional();
        }
    }
}
