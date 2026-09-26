using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityApp.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<Course> Courses { get; set; } = null!;
    }
}