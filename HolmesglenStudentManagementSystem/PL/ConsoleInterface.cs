using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using HolmesglenStudentManagementSystem.BLL;
using HolmesglenStudentManagementSystem.DAL;
using HolmesglenStudentManagementSystem.Models;
namespace HolmesglenStudentManagementSystem.PL
{
    public class ConsoleInterface
    {
     
    private readonly StudentBLL _studentBLL;
  
        private readonly SubjectBLL _subjectBLL;
        private readonly EnrollmentBLL _enrollmentBLL;
        private readonly GenerateReportBLL _generateReportBLL;
    
        public ConsoleInterface()
        {
            _enrollmentBLL = new EnrollmentBLL();

        }
        private StudentBLL studentBLL = new StudentBLL();
        private SubjectBLL subjectBLL = new SubjectBLL();
        private EnrollmentBLL enrolmentBLL = new EnrollmentBLL(); 
        private GenerateReportBLL generateReportBLL  = new GenerateReportBLL(); 

               

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Holmesglen Student Management System");
                Console.WriteLine("1. Student Management");
                Console.WriteLine("2. Subject Management");
                Console.WriteLine("3. Enrolment Management");
                Console.WriteLine("4. Generate Enrollment Report");
                Console.WriteLine("5. Send Enrollment Email");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ShowStudentMenu();
                        break;
                    case "2":
                        ShowSubjectMenu();
                        break;
                    case "3":
                        ShowEnrolmentMenu();
                        break;
                    case "4":
                        GenerateReport();
                        break;
                    case "5":
                        Console.Write("Enter student ID: ");
                        string studentId = Console.ReadLine();
                        SendEnrollmentEmail(studentId);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }



        private void ShowStudentMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Student");
                Console.WriteLine("3. View All Students");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Back");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            AddStudent();
                            break;
                        case "2":
                            ViewStudent();
                            break;
                        case "3":
                            ViewAllStudents();
                            break;
                        case "4":
                            UpdateStudent();
                            break;
                        case "5":
                            DeleteStudent();
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

   

        private void ShowSubjectMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Add Subject");
                Console.WriteLine("2. View Subject");
                Console.WriteLine("3. View All Subjects");
                Console.WriteLine("4. Update Subject");
                Console.WriteLine("5. Delete Subject");
                Console.WriteLine("6. Back");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            AddSubject();
                            break;
                        case "2":
                            ViewSubject();
                            break;
                        case "3":
                            ViewAllSubjects();
                            break;
                        case "4":
                            UpdateSubject();
                            break;
                        case "5":
                            DeleteSubject();
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    

        private void ShowEnrolmentMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Add Enrolment");
                Console.WriteLine("2. View Enrolment");
                Console.WriteLine("3. View All Enrolments");
                Console.WriteLine("4. Update Enrolment");
                Console.WriteLine("5. Delete Enrolment");
                Console.WriteLine("6. Back");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            AddEnrolment();
                            break;
                        case "2":
                            ViewEnrolment();
                            break;
                        case "3":
                            ViewAllEnrollments();
                            break;
                        case "4":
                            UpdateEnrolment();
                            break;
                        case "5":
                            DeleteEnrolment();
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }



        private void AddStudent()
        {
            var student = new Student();

            Console.Write("Enter first name: ");
            student.FirstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            student.LastName = Console.ReadLine();

            Console.Write("Enter email address: ");
            student.EmailAddress = Console.ReadLine();

            Console.Write("Enter age: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                student.Age = age;
            }
            else
            {
                Console.WriteLine("Invalid age. Operation aborted.");
                return;
            }

            student.EnrolmentDate = DateTime.Now;

            studentBLL.CreateStudent(student);
            Console.WriteLine("Student added successfully.");
        }

        private void ViewStudent()
        {
       Console.Write("Enter student ID: ");
        string studentId = Console.ReadLine();
        var student = studentBLL.GetStudentById(studentId);
        if (student != null)
                {
                    Console.WriteLine($"ID: {student.StudentID}");
                    Console.WriteLine($"Name: {student.FirstName} {student.LastName}");
                    Console.WriteLine($"Email: {student.EmailAddress}");
                    Console.WriteLine($"Age: {student.Age}");
                    Console.WriteLine($"Enrolment Date: {student.EnrolmentDate}");
                }
            
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private void ViewAllStudents()
        {
            var students = studentBLL.GetAllStudents();
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.StudentID} - Name: {student.FirstName} {student.LastName}");
            }
        }

        private void UpdateStudent()
        {
            Console.Write("Enter student ID: ");
            string studentId = Console.ReadLine();
            var student = studentBLL.GetStudentById(studentId);
            if (student != null)
                {
                    Console.Write("Enter new first name (leave empty to keep current): ");
                    var firstName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(firstName))
                    {
                        student.FirstName = firstName;
                    }

                    Console.Write("Enter new last name (leave empty to keep current): ");
                    var lastName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(lastName))
                    {
                        student.LastName = lastName;
                    }

                    Console.Write("Enter new email address (leave empty to keep current): ");
                    var email = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        student.EmailAddress = email;
                    }

                    Console.Write("Enter new age (leave empty to keep current): ");
                    var ageInput = Console.ReadLine();
                    if (int.TryParse(ageInput, out int age))
                    {
                        student.Age = age;
                    }

                    studentBLL.UpdateStudent(student);
                    Console.WriteLine("Student updated successfully.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
        
            
        }

        private void DeleteStudent()
        {
            Console.Write("Enter student ID: ");
            string studentId = Console.ReadLine();
            var student = studentBLL.GetStudentById(studentId);
            if (student != null)
            {
                studentBLL.DeleteStudent(studentId);
                Console.WriteLine("Student deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private void AddSubject()
        {
            var subject = new Subject();

            Console.Write("Enter title: ");
            subject.Title = Console.ReadLine();

            Console.Write("Enter number of sessions: ");
            if (int.TryParse(Console.ReadLine(), out int numberOfSessions))
            {
                subject.NumberOfSessions = numberOfSessions;
            }
            else
            {
                Console.WriteLine("Invalid number of sessions. Operation aborted.");
                return;
            }

            Console.Write("Enter hours per session: ");
            if (double.TryParse(Console.ReadLine(), out double hourPerSession))
            {
                subject.HourPerSession = hourPerSession;
            }
            else
            {
                Console.WriteLine("Invalid hours per session. Operation aborted.");
                return;
            }

            subjectBLL.CreateSubject(subject);
            Console.WriteLine("Subject added successfully.");
        }

        private void ViewSubject()
        {
            
            Console.Write("Enter subject ID: ");
            string subjectId = Console.ReadLine();
            var subject = subjectBLL.GetSubjectById(subjectId);
            if (subject != null)
                {
                    Console.WriteLine($"ID: {subject.SubjectID}");
                    Console.WriteLine($"Title: {subject.Title}");
                    Console.WriteLine($"Number of Sessions: {subject.NumberOfSessions}");
                    Console.WriteLine($"Hours per Session: {subject.HourPerSession}");
                }
                else
                {
                    Console.WriteLine("Subject not found.");
                }
            }
  
        

        private void ViewAllSubjects()
        {
            var subjects = subjectBLL.GetAllSubjects();
            foreach (var subject in subjects)
            {
                Console.WriteLine($"ID: {subject.SubjectID} - Title: {subject.Title}");
            }
        }
        

        private void UpdateSubject()
        {
            Console.Write("Enter subject ID: ");
            string subjectId = Console.ReadLine();
            var subject = subjectBLL.GetSubjectById(subjectId);
            
                if (subject != null)
                {
                    Console.Write("Enter new title (leave empty to keep current): ");
                    var title = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(title))
                    {
                        subject.Title = title;
                    }

                    Console.Write("Enter new number of sessions (leave empty to keep current): ");
                    var numberOfSessionsInput = Console.ReadLine();
                    if (int.TryParse(numberOfSessionsInput, out int numberOfSessions))
                    {
                        subject.NumberOfSessions = numberOfSessions;
                    }

                    Console.Write("Enter new hours per session (leave empty to keep current): ");
                    var hourPerSessionInput = Console.ReadLine();
                    if (double.TryParse(hourPerSessionInput, out double hourPerSession))
                    {
                        subject.HourPerSession = hourPerSession;
                    }

                    subjectBLL.UpdateSubject(subject);
                    Console.WriteLine("Subject updated successfully.");
                }
                else
                {
                    Console.WriteLine("Subject not found.");
                }
        
        
        }
    
        

        private void DeleteSubject()
        {
            Console.Write("Enter subject ID: ");
            string subjectId = Console.ReadLine();
            var subject = subjectBLL.GetSubjectById(subjectId);
            if (subject != null)
            {
                subjectBLL.DeleteSubject(subjectId);
                Console.WriteLine("Subject deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }


        private void ViewAllEnrollments()
        {
            var enrollments = enrolmentBLL.GetAllEnrollments();
            foreach (var enrollment in enrollments)
            {
                Console.WriteLine($"Enrollment ID: {enrollment.ID}");
                Console.WriteLine($"Student ID: {enrollment.StudentID}");
                Console.WriteLine($"Subject ID: {enrollment.SubjectID}");
                Console.WriteLine();
            }
    

        }


        private void ViewEnrolment()
        {
            Console.Write("Enter enrolment ID: ");
            if (int.TryParse(Console.ReadLine(), out int enrolmentId))
            {
            var enrolment = enrolmentBLL.GetEnrollmentById(enrolmentId);
            if (enrolment != null)
            {
                Console.WriteLine($"ID: {enrolment.ID}");
                Console.WriteLine($"Student ID: {enrolment.StudentID}");
                Console.WriteLine($"Subject ID: {enrolment.SubjectID}");
            }
            else
            {
                Console.WriteLine("Enrolment not found.");
            }
            }
            else
            {
            Console.WriteLine("Invalid ID.");
            }
        }

        private void AddEnrolment()
        {
            var enrolment = new Enrollment();
            Console.Write("Enter student ID: ");
            if (int.TryParse(Console.ReadLine(), out int studentId))
            {
                enrolment.StudentID = studentId.ToString();
            }
            else
            {
                Console.WriteLine("Invalid student ID. Operation aborted.");
                return;
            }
            
            Console.Write("Enter subject ID: ");
            if (int.TryParse(Console.ReadLine(), out int subjectId))
            {
                enrolment.SubjectID = subjectId.ToString();
            }
            else
            {
                Console.WriteLine("Invalid subject ID. Operation aborted.");
                return;
            }

            enrolment.EnrolmentDate = DateTime.Now;
            enrolmentBLL.CreateEnrollment(enrolment);
            Console.WriteLine("Enrolment added successfully.");
        }

        private void UpdateEnrolment()
        {
            Console.Write("Enter enrolment ID: ");
            if (int.TryParse(Console.ReadLine(), out int enrolmentId))
            {
            var enrolment = enrolmentBLL.GetEnrollmentById(enrolmentId);
            if (enrolment != null)
            {
                Console.Write("Enter new student ID (leave empty to keep current): ");
                var studentIdInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(studentIdInput))
                {
                if (int.TryParse(studentIdInput, out int studentId))
                {
                    enrolment.StudentID = studentId.ToString();
                }
                else
                {
                    Console.WriteLine("Invalid student ID. Operation aborted.");
                    return;
                }
                }

                Console.Write("Enter new subject ID (leave empty to keep current): ");
                var subjectIdInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(subjectIdInput))
                {
                if (int.TryParse(subjectIdInput, out int subjectId))
                {
                    enrolment.SubjectID = subjectId.ToString();
                }
                else
                {
                    Console.WriteLine("Invalid subject ID. Operation aborted.");
                    return;
                }
                }

                enrolmentBLL.UpdateEnrollment(enrolment);
                Console.WriteLine("Enrolment updated successfully.");
            }
            else
            {
                Console.WriteLine("Enrolment not found.");
            }
            }
            else
            {
            Console.WriteLine("Invalid ID.");
            }

        }
        private void DeleteEnrolment()
        {
            Console.Write("Enter enrolment ID: ");
            if (int.TryParse(Console.ReadLine(), out int enrolmentId))
            {
            enrolmentBLL.DeleteEnrollment(enrolmentId);
            Console.WriteLine("Enrolment deleted successfully.");
            }
            else
            {
            Console.WriteLine("Invalid ID.");
            }
        }
        private void GenerateReport()
        {
         Console.WriteLine("Enrollment Report");

            var generateReportBLL = new GenerateReportBLL();
            var result = generateReportBLL.GetAll();

            if (result.Count == 0)
            {
                Console.WriteLine("No data is found");
            }
            else
            {
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.StudentID}\t{item.FirstName}\t{item.LastName}\t{item.SubjectTitle}\t{item.SubjectID}");
                }
            }
        }
    
        private void SendEnrollmentEmail(string studentId)
        {
         
        string connectionString = @"Data Source=C:\Users\623209788\OneDrive - Holmesglen Institute\C#\AssessmentTask2\Project2\HolmesglenStudentManagementSystem\HolmesglenStudentManagementSystem.db;";
        string studentName = "";
        List<string> subjects = new List<string>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Get student name
            using (SqlCommand command = new SqlCommand($"SELECT Name FROM Students WHERE Id = '{studentId}'", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    studentName = reader.GetString(0);
                }
                reader.Close();
            }

            // Get subjects
            using (SqlCommand command = new SqlCommand($"SELECT SubjectName, SubjectId FROM Enrollment WHERE StudentId = '{studentId}'", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    subjects.Add($"Subject: {reader.GetString(0)} (SubjectID: {reader.GetString(1)})");
                }
                reader.Close();
            }
        }

        // Generate email
        Console.WriteLine($"Dear {studentName},");
        Console.WriteLine("\nYou have been enrolled in the following subjects");
        foreach (string subject in subjects)
        {
            Console.WriteLine($"> {subject}");
        }
        Console.WriteLine("Please login to your account and confirm the above enrollments.\n");
        Console.WriteLine("Regards,");
        Console.WriteLine("CAIT Department");
    }


    
    }
}
        

    


    
