// .NET 22 Daniel Svensson
using System;
using System.Collections.Generic;

namespace Database_Project.Models
{
    public partial class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? StaffId { get; set; }
        public int? GradeId { get; set; }
        public int? StudentId { get; set; }
        public int? IsActive { get; set; }
    }
}
