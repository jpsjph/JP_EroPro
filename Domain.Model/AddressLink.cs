using Core.Infrastructure;
using Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ClientAddress : EntityBaseFields
    {
        public ClientAddress()
        {
            Clients = new HashSet<Client>();
            Addresses = new HashSet<Address>();
        }
        public int ClientAddressId { get; set; }
        public bool IsDefault { get; set; }
        public AddressType AddressType { get; set; }
     
        public ICollection<Client> Clients { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
