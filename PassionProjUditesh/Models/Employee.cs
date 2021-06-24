using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProjUditesh.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string EmployeeMobNum { get; set; }


        public string EmployeeEmail { get; set; }

        public string EmployeeAdrress { get; set; }

        [ForeignKey("Sector")]
        public int SectorID { get; set; }
        public virtual Sector Sector { get; set; }
    }

    public class EmployeeDto
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string EmployeeMobNum { get; set; }


        public string EmployeeEmail { get; set; }

        public string EmployeeAdrress { get; set; }
        public int SectorID { get; set; }
        public string SectorName { get; set; }

    }


}