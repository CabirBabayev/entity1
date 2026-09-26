using Microsoft.EntityFrameworkCore;
using UniversityApp.Context;
using UniversityApp.Entities;

using var context = new AppDbContext();
if (!context.Teachers.Any())
{
    var teachers = new List<Teacher>
    {
        new Teacher
        {
            Name = "John",
            Email = "john@gmail.com"
        },
        new Teacher
        {
            Name = "Ali",
            Email = "ali@gmail.com"
        },
        new Teacher
        {
            Name = "David",
            Email = "david@gmail.com"
        }
    };
    context.Teachers.AddRange(teachers);
    context.SaveChanges();
}
if (!context.Students.Any())
{
    var students = new List<Student>
    {
        new Student
        {
            Name = "Ali",
            Age = 20,
            Email = "ali@student.com"
        },
        new Student
        {
            Name = "Vali",
            Age = 21,
            Email = "vali@student.com"
        },
        new Student
        {
            Name = "Murad",
            Age = 19,
            Email = "murad@student.com"
        },
        new Student
        {
            Name = "Leyla",
            Age = 22,
            Email = "leyla@student.com"
        },
        new Student
        {
            Name = "Aysel",
            Age = 20,
            Email = "aysel@student.com"
        }
    };
    context.Students.AddRange(students);
    context.SaveChanges();
}

if (!context.StudentCards.Any())
{
    var students = context.Students.ToList();
    var cards = new List<StudentCard>
    {
        new StudentCard
        {
            CardNumber = "SC001",
            IssueDate = DateTime.Now,
            StudentId = students[0].Id
        },
        new StudentCard
        {
            CardNumber = "SC002",
            IssueDate = DateTime.Now,
            StudentId = students[1].Id
        },
        new StudentCard
        {
            CardNumber = "SC003",
            IssueDate = DateTime.Now,
            StudentId = students[2].Id
        },
        new StudentCard
        {
            CardNumber = "SC004",
            IssueDate = DateTime.Now,
            StudentId = students[3].Id
        },
        new StudentCard
        {
            CardNumber = "SC005",
            IssueDate = DateTime.Now,
            StudentId = students[4].Id
        }
    };
    context.StudentCards.AddRange(cards);
    context.SaveChanges();
}

if (!context.Courses.Any())
{
    var teachers = context.Teachers.ToList();
    var courses = new List<Course>
    {
        new Course
        {
            Name = "C#",
            Description = "C# programming",
            TeacherId = teachers[0].Id
        },
        new Course
        {
            Name = "EF Core",
            Description = "Entity Framework Core",
            TeacherId = teachers[0].Id
        },
        new Course
        {
            Name = "SQL",
            Description = "SQL and databases",
            TeacherId = teachers[1].Id
        },
        new Course
        {
            Name = "Algorithms",
            Description = "Algorithms and data structures",
            TeacherId = teachers[1].Id
        },
        new Course
        {
            Name = "Programming",
            Description = "Basic programming",
            TeacherId = teachers[2].Id
        }
    };
    context.Courses.AddRange(courses);
    context.SaveChanges();
}

if (!context.StudentCourses.Any())
{
    var students = context.Students.ToList();
    var courses = context.Courses.ToList();
    var studentCourses = new List<StudentCourse>
    {
        new StudentCourse
        {
            StudentId = students[0].Id,
            CourseId = courses[0].Id
        },
        new StudentCourse
        {
            StudentId = students[0].Id,
            CourseId = courses[1].Id
        },
        new StudentCourse
        {
            StudentId = students[0].Id,
            CourseId = courses[2].Id
        },
        new StudentCourse
        {
            StudentId = students[1].Id,
            CourseId = courses[0].Id
        },
        new StudentCourse
        {
            StudentId = students[1].Id,
            CourseId = courses[3].Id
        },
        new StudentCourse
        {
            StudentId = students[2].Id,
            CourseId = courses[1].Id
        },
        new StudentCourse
        {
            StudentId = students[2].Id,
            CourseId = courses[2].Id
        },
        new StudentCourse
        {
            StudentId = students[3].Id,
            CourseId = courses[3].Id
        },
        new StudentCourse
        {
            StudentId = students[3].Id,
            CourseId = courses[4].Id
        },
        new StudentCourse
        {
            StudentId = students[4].Id,
            CourseId = courses[0].Id
        },
        new StudentCourse
        {
            StudentId = students[4].Id,
            CourseId = courses[4].Id
        }
    };
    context.StudentCourses.AddRange(studentCourses);
    context.SaveChanges();
}

var allStudents = context.Students.ToList();
foreach (var student in allStudents)
{
    Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}, Email: {student.Email}");
}


var studentsWithCards = context.Students
    .Include(s => s.StudentCard)
    .ToList();

foreach (var student in studentsWithCards)
{
    Console.WriteLine($"{student.Name} — Card: {student.StudentCard?.CardNumber}");
}

var coursesWithTeachers = context.Courses
    .Include(c => c.Teacher)
    .ToList();

foreach (var course in coursesWithTeachers)
{
    Console.WriteLine($"{course.Name} — Teacher: {course.Teacher.Name}");
}

var studentsWithCourses = context.Students
    .Include(s => s.StudentCourses)
    .ThenInclude(sc => sc.Course)
    .ToList();

foreach (var student in studentsWithCourses)
{
    Console.WriteLine($"\n{student.Name}:");

    foreach (var studentCourse in student.StudentCourses)
    {
        Console.WriteLine($"    {studentCourse.Course.Name}");
    }
}

var existingTeacher = context.Teachers.First();
var newCourse = new Course
{
    Name = "ASP.NET Core",
    Description = "Web development with ASP.NET Core",
    TeacherId = existingTeacher.Id
};

context.Courses.Add(newCourse);

Console.WriteLine($"Новый курс до SaveChanges: " + $"{context.Entry(newCourse).State}");
context.SaveChanges();
Console.WriteLine( $"Новый курс до SaveChanges: " + $"{context.Entry(newCourse).State}");

var testStudent = new Student
{
    Name = "Test Student",
    Age = 20,
    Email = "test@test.com"
};
Console.WriteLine($"До добавления: {context.Entry(testStudent).State}");
context.Students.Add(testStudent);
Console.WriteLine($"После добавления: {context.Entry(testStudent).State}");
context.SaveChanges();
Console.WriteLine($"После SaveChanges: {context.Entry(testStudent).State}");

//modified cto to tam
var studentToModify = context.Students
    .First(x => x.Id == 1);
Console.WriteLine($"До модивик: {context.Entry(studentToModify).State}");
studentToModify.Name = "New Name";

Console.WriteLine($"После модивик: {context.Entry(studentToModify).State}");
context.SaveChanges();
Console.WriteLine($"Посде SaveChanges: {context.Entry(studentToModify).State}");

//delete delayem
var studentToDelete = context.Students
    .FirstOrDefault(x => x.Email == "test@test.com");
if (studentToDelete != null)
{
    context.Students.Remove(studentToDelete);
    Console.WriteLine($"До SaveChanges: {context.Entry(studentToDelete).State}");
    context.SaveChanges();
    Console.WriteLine($"После SaveChanges: {context.Entry(studentToDelete).State}");
}


//ChangeTracker.Entries()
var studentForTracker = context.Students
    .First(x => x.Id == 2);

studentForTracker.Name = "Noviy Vali";

var teacherForTracker = context.Teachers
    .First(x => x.Id == 1);

teacherForTracker.Name = "Noviy John";

var courseForTracker = new Course
{
    Name = "İntersniy Kurs",
    Description = "Ocen interesno",
    TeacherId = teacherForTracker.Id
};
context.Courses.Add(courseForTracker);
foreach (var entry in context.ChangeTracker.Entries())
{
    Console.WriteLine($"{entry.Entity.GetType().Name} - {entry.State}");
}
context.SaveChanges();

//Cepocka dannix
var teachersWithStudents = context.Teachers
    .Include(t => t.Courses)
        .ThenInclude(c => c.StudentCourses)
            .ThenInclude(sc => sc.Student)
    .ToList();

foreach (var teacher in teachersWithStudents)
{
    Console.WriteLine($"\nTeacher: {teacher.Name}");

    foreach (var course in teacher.Courses)
    {
        Console.WriteLine($"  Course: {course.Name}");
        Console.WriteLine("  Students:");

        foreach (var studentCourse in course.StudentCourses)
        {
            Console.WriteLine($"      {studentCourse.Student.Name}");
        }
    }
}
