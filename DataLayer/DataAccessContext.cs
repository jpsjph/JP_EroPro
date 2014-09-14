using Core.Concrete;
using Domain.Model;
using Domain.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
    public class DataAccessContext :DataContext
    {
        static DataAccessContext()
        {
            Database.SetInitializer<DataAccessContext>(new DropCreateDatabaseAlways<DataAccessContext>());
        }
        public DataAccessContext()
            : base("Name=DataContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new EmailMap());
            modelBuilder.Configurations.Add(new PhoneMap());
            modelBuilder.Configurations.Add(new TransactionPaymentMap());
            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new TransactionStatusMap());
            modelBuilder.Configurations.Add(new TransactionSubStatusMap());
            modelBuilder.Configurations.Add(new IndividualMap());
        }

        public DbSet<Individual> Individual { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<TransactionPayment> Transactions { get; set; }
        public DbSet<EmailAddress> EmailAddress { get; set; }

      
    }
}
