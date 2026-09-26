using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityApp.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Email { get; set; } = null!;
        public List<StudentCourse> StudentCourses { get; set; } = null!;
        public StudentCard StudentCard { get; set; } = null!;
        //номр надо
        public string PhoneNumber { get; set; } = null!;
    }
}