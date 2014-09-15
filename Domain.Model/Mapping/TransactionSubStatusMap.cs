using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Mapping
{
    public class TransactionSubStatusMap : EntityTypeConfiguration<TransactionSubStatus>
    {
        public TransactionSubStatusMap()
        {
            this.HasKey(k => k.SubStatusId);
            this.Property(p => p.CreatorId).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.UpdatorId).IsOptional();
            this.Property(p => p.DateUpdated).IsOptional();
        }
    }
}
