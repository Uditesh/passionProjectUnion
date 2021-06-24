using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProjUditesh.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public string ArrivedFrom { get; set; }
        public string DepartureTo { get; set; }
        public DateTime OrderDateTime { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [ForeignKey("Sector")]
        public int SectorID { get; set; }
        public virtual Sector Sector { get; set; }
    }

    public class OrderDto
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public string ArrivedFrom { get; set; }
        public string DepartureTo { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int CustomerID { get; set; }
        public int SectorID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobNum { get; set; }

    }
}