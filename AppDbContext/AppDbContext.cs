using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.AppDbContext
{
    public class WebAppDbContext: DbContext
    {
        public WebAppDbContext(): base("AppConnectionString") 
        { 
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}