using PixelDrift.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PixelDrift.Data
{
    public partial class AppDbContext:DbContext
    {

        public AppDbContext() :   base("name=pixeldrift_dbEntities1")
        {

        }

        public IDbSet<User_login> user_Logins { get; set; }
        public IDbSet<Person> Persons { get; set; }

        public IDbSet<ImageSave> ImageSave { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }
}