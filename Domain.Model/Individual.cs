using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Enums;
using Core.Infrastructure;

namespace Domain.Model
{
    public class Individual : EntityBaseFields
    {
        public int IndividualId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DefaultEmail { get; set; }
        public string DefaultAddress { get; set; }
        public Status IndividualStatus { get; set; }       
        public Contact Contact { get; set; }
    }
}
