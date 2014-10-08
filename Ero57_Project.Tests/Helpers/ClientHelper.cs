using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ero57_Project.Tests.Helpers
{
    public class ClientHelper
    {
        public static List<Client> GetClients()
        {
            return new List<Client>{
                new Client{ID=3, Company="Test1", Address1="london1"}
            };
        }

        public static Client GetClientById(int id)
        {
            return GetClients().FirstOrDefault(x => x.ID == id);
        }
    }
}