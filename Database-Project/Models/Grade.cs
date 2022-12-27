using System;
using System.Collections.Generic;

namespace Database_Project.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public int? Grade1 { get; set; }
        public int? CourseId { get; set; }
        public int? StaffId { get; set; }
        public DateTime? GradeDate { get; set; }
    }
}
