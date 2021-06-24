using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PassionProjUditesh.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string CustomerMobNum { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerAddress { get; set; }
    }

    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string CustomerMobNum { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerAddress { get; set; }

    }
}