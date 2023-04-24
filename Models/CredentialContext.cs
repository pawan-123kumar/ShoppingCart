using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class CredentialsContext : DbContext
    {
        public CredentialsContext() : base("name=MyConnectionString")
        {

        }

        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}