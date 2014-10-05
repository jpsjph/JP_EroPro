using System.Collections.Generic;
using System.Linq;
using Common.Services.ViewModel;
using Domain.Model;

namespace Ero57_API.Tests.Helpers
{
    public class ContactHelper
    {
        public static List<Contacts> GetContactLists()
        {
            return new List<Contacts>
           {
               new Contacts{ID=5877,ContactName = "John",Telephone = "414-1154545-521",Chase = true,ContactRef = "Ref14255",ContactTypes = GetContactTypeList().FirstOrDefault(),Debtor = 2,Email = "jpsjp@google.com"},
               new Contacts{ID=9977,ContactName = "Kelvin",Telephone = "114-1124555-021",Chase = false,ContactRef = "14255",ContactTypes = GetContactTypeList().FirstOrDefault(x=>x.ID==3),Debtor = 80099,Email = "test@google.com"},
               new Contacts{ID=5822,ContactName = "Jean-Pierre",Telephone = "400-0045450-521",Chase = true,ContactRef = "Ref9955",ContactTypes = GetContactTypeList().FirstOrDefault(x=>x.ID==2),Debtor = 8399,Email = "jp@google.com"},
               new Contacts{ID=5833,ContactName = "Steve",Telephone = "224-1154545-331",Chase = false,ContactRef = "Ref88255",ContactTypes = GetContactTypeList().FirstOrDefault(x=>x.ID==4),Debtor = 18099,Email = "steve@google.com"}
           };
        }

        public static List<ContactTypes> GetContactTypeList()
        {
            return new List<ContactTypes>
            {
                new ContactTypes {ID = 1, ContactType = "Accounts", DisplayOrder = 1},
                new ContactTypes {ID = 2, ContactType = "Director", DisplayOrder = 11},
                new ContactTypes {ID = 3, ContactType = "Marketing", DisplayOrder = 10},
                new ContactTypes {ID = 4, ContactType = "Unknown", DisplayOrder = 2}
            };
        }

        public static ContactModel GetContactModel()
        {
            return new ContactModel
            {
                Id = 5877,
                ContactName = "John",
                Telephone = "414-1154545-521",
                Chase = true,
                ContactRef = "Ref14255",
                ContactType = 2,
                Debtor = 83399,
                Email = "jpsjp@google.com"
            };
        }

        public static List<Debtor> GetDebtorList()
        {
            return new List<Debtor>
            {
                new Debtor {ID = 2, AccountCode = "12455145442", Address1 = "Namur", SelectedDunningContact = 5877},
                new Debtor {ID = 1, AccountCode = "124578987005", Address1 = "BXL", SelectedDunningContact = 5833},
                new Debtor {ID = 22, AccountCode = "1245514585454", Address1 = "London", SelectedDunningContact = 5834},
                new Debtor {ID = 12, AccountCode = "12455144415", Address1 = "Tkyo", SelectedDunningContact = 5884},
                new Debtor {ID = 112, AccountCode = "12455112145", Address1 = "NY", SelectedDunningContact = 5836}
            };
        }
    }
}
