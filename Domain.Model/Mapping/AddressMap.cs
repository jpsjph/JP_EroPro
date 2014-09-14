using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Domain.Model.Mapping
{
    public class AddressMap: EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {

            this.Property(p => p.CreatorId).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.UpdatorId).IsOptional();
            this.Property(p => p.DateUpdated).IsOptional();
        }
    }
}
