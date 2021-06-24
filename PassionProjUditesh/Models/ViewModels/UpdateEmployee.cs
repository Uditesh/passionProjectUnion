 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjUditesh.Models.ViewModels
{
    public class UpdateEmployee
    {
        public EmployeeDto SelectedEmployee { get; set; }
        public IEnumerable<SectorDto> SectorOptions { get; set; }
    }
}