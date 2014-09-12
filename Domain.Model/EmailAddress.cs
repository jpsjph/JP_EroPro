using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class EmailAddress : EntityBase
    {
        public int EmailAddressId { get; set; }
        public string Email { get; set; }
        public bool IsDefault { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CreatorId { get; set; }
        public int UpdatorId { get; set; }
        public ICollection<Individual> Individuals { get; set; }
    }
}
