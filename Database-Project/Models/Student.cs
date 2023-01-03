// .NET 22 Daniel Svensson
using System;
using System.Collections.Generic;

namespace Database_Project.Models
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public string? Firstname { get; set; }
        public string? LastName { get; set; }
        public int? ClassYear { get; set; }
    }
}
