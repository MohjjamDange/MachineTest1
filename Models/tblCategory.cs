using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineTest1.Models
{
    public class tblCategory
    {
        [Key]
        public int category_id { get; set; }   
        public string category_name { get; set; }
        public List<tblProduct> tblProducts { get; set; }
    }
}