using System;
using System.Collections.Generic;

namespace Database_Project.Models
{
    public partial class Department
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? StaffId { get; set; }
        public int? CourseId { get; set; }
    }
}
