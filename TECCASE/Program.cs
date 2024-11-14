using TECCASE.Enums;
using TECCASE.Models;

namespace TECCASE
{
    public class Program
    
        {
            static List<Teacher> teachers = new List<Teacher>();
            static List<Student> students = new List<Student>();
            static List<Course> courses = new List<Course>();

            static void Main(string[] args)
            {
                InitializeData();

                while (true)
                {
                    Console.WriteLine("Choose a search option:");
                    Console.WriteLine("1. Search by Teacher");
                    Console.WriteLine("2. Search by Student");
                    Console.WriteLine("3. Search by Course");
                    Console.Write("Enter choice (1-3): ");
                    int choice = int.TryParse(Console.ReadLine(), out var result) ? result : 0;

                    switch ((SearchCriteria)choice)
                    {
                        case SearchCriteria.Teacher:
                            SearchByTeacher();
                            break;
                        case SearchCriteria.Student:
                            SearchByStudent();
                            break;
                        case SearchCriteria.Course:
                            SearchByCourse();
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
            }

            static void InitializeData()
            {
                var teacher1 = new Teacher(1, "John", "Doe");
                var teacher2 = new Teacher(2, "Jane", "Smith");
                teachers.AddRange(new[] { teacher1, teacher2 });

                var student1 = new Student("Alice", "Johnson", new DateTime(2005, 4, 12));
                var student2 = new Student("Bob", "Brown", new DateTime(2003, 6, 15));
                students.AddRange(new[] { student1, student2 });

                var course1 = new Course { Name = "Mathematics", Teacher = teacher1, Students = new List<Student> { student1, student2 } };
                var course2 = new Course { Name = "Science", Teacher = teacher2, Students = new List<Student> { student2 } };
                courses.AddRange(new[] { course1, course2 });

                teacher1.AddCourse(course1);
                teacher2.AddCourse(course2);
            }

            static void SearchByTeacher()
            {
                Console.WriteLine("Available Teachers:");
                teachers.OrderBy(t => t.FirstName).ToList().ForEach(t => Console.WriteLine($"ID: {t.Id}, Name: {t.FirstName} {t.LastName}"));

                Console.Write("Enter Teacher ID: ");
                if (int.TryParse(Console.ReadLine(), out int teacherId))
                {
                    var teacher = teachers.FirstOrDefault(t => t.Id == teacherId);
                    if (teacher == null)
                    {
                        Console.WriteLine("No match found.");
                        return;
                    }

                    Console.WriteLine($"Courses taught by {teacher.FirstName} {teacher.LastName}:");
                    foreach (var course in teacher.Courses)
                    {
                        Console.WriteLine($"Course: {course.Name}");
                        Console.WriteLine($"Students Enrolled: {course.Students.Count}");
                        foreach (var student in course.Students)
                        {
                            var color = student.Age < 20 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.ForegroundColor = color;
                            Console.WriteLine($" - {student.FirstName} {student.LastName}");
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID.");
                }
            }

            static void SearchByStudent()
            {
                Console.Write("Enter Full Name of Student: ");
                var studentName = Console.ReadLine();
                var student = students.FirstOrDefault(s => $"{s.FirstName} {s.LastName}".Equals(studentName, StringComparison.OrdinalIgnoreCase));

                if (student == null)
                {
                    Console.WriteLine("No match found.");
                    return;
                }

                Console.WriteLine($"Courses for {student.FirstName} {student.LastName}:");
                foreach (var course in courses.Where(c => c.Students.Contains(student)))
                {
                    Console.WriteLine($"- {course.Name} (Teacher: {course.Teacher.FirstName} {course.Teacher.LastName})");
                }
            }

            static void SearchByCourse()
            {
                Console.WriteLine("Available Courses:");
                courses.ForEach(c => Console.WriteLine($"Course: {c.Name}"));

                Console.Write("Enter Course Name: ");
                var courseName = Console.ReadLine();
                var course = courses.FirstOrDefault(c => c.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase));

                if (course == null)
                {
                    Console.WriteLine("No match found.");
                    return;
                }

                Console.WriteLine($"Course: {course.Name}");
                Console.WriteLine($"Teacher: {course.Teacher.FirstName} {course.Teacher.LastName}");
                Console.WriteLine($"Enrolled Students: {course.Students.Count}");
                foreach (var student in course.Students)
                {
                    var color = student.Age < 20 ? ConsoleColor.Red : ConsoleColor.White;
                    Console.ForegroundColor = color;
                    Console.WriteLine($"- {student.FirstName} {student.LastName}");
                    Console.ResetColor();
                }
            }
        }
    }

