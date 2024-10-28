using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineTest1.Models
{
    
    public class tblProduct
    {
        [Key]
        public int product_id { get; set; }
        public string product_name { get; set; }

        [ForeignKey("tblCategory")]
        public int category_id { get; set; }
        public tblCategory tblCategory { get; set; }
    }
}