using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityApp.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public List<StudentCourse> StudentCourses { get; set; } = null!;
    }
}