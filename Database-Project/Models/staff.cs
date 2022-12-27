using System;
using System.Collections.Generic;

namespace Database_Project.Models
{
    public partial class staff
    {
        public int StaffId { get; set; }
        public string? Occupation { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Hiredate { get; set; }
        public int? DepartmentId { get; set; }
        public double? Salary { get; set; }
    }
}
