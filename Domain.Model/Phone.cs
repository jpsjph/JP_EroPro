using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Phone : EntityBaseFields
    {
        public Phone()
        {
            Individuals = new HashSet<Individual>();
        }
        public int PhoneId { get; set; }
        public string Number { get; set; }
        public bool IsDefault { get; set; }        
        public virtual ICollection<Individual> Individuals { get; set; }
    }
}
