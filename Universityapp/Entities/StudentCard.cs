using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityApp.Entities
{
    public class StudentCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}