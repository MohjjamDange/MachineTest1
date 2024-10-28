using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Xml.Linq;
using System.Web.UI.WebControls.WebParts;

namespace MachineTest1.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(): base("name = MyConn")
        {
          

        }

        public DbSet<tblCategory> tblCategories { get; set; }
        public DbSet<tblProduct> tblProducts { get; set; }

       

    }
}