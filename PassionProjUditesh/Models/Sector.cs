using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProjUditesh.Models
{
    public class Sector
    {
        [Key]
        public int SectorID { get; set; }

        public string SectorName { get; set; }
    }
    public class SectorDto
    {
        public int SectorID { get; set; }

        public string SectorName { get; set; }
    }
}