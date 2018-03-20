using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project.Models.VM
{
    public class OrderVM
    {
        public int OrderID { get; set; }
        public string Username { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        //public List<OrederDetails> OderDetails { get; set; }
    }
}