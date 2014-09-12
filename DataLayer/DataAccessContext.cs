using Core.Concrete;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
    public class DataAccessContext : DataContext
    {
        static DataAccessContext()
        {
            Database.SetInitializer<DataAccessContext>(null);
        }
        public DataAccessContext(): base("Name=DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Individual>().ToTable("Individual");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Individual> Individual { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Contact> Contact { get; set; }
        //public DbSet<Registration> Registration { get; set; }
        public DbSet<EmailAddress> EmailAddress { get; set; }
    }
}
