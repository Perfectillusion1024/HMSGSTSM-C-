using System.Data.SqlClient;
using HolmesglenStudentManagementSystem.Models;
using Microsoft.Data.Sqlite;

namespace HolmesglenStudentManagementSystem.DAL
{
    public class StudentDAL
    {
private string connectionString = @"Data Source=C:\Users\623209788\OneDrive - Holmesglen Institute\C#\AssessmentTask2\Project2\HolmesglenStudentManagementSystem\HolmesglenStudentManagementSystem.db;";

        public void CreateStudent(Student student)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand(
                    "INSERT INTO Student (FirstName, LastName, EmailAddress, Age, EnrolmentDate) VALUES (@FirstName, @LastName, @EmailAddress, @Age, @EnrolmentDate)",
                    connection);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@EmailAddress", student.EmailAddress);
                command.Parameters.AddWithValue("@Age", student.Age);
                command.Parameters.AddWithValue("@EnrolmentDate", student.EnrolmentDate);
                command.ExecuteNonQuery();
            }
        }

        public Student ReadStudent(string studentId)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM Student WHERE StudentID = @StudentID", connection);
                command.Parameters.AddWithValue("@StudentID", studentId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student
                        {
                            StudentID = reader.GetString(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            EmailAddress = reader.GetString(3),
                            Age = reader.GetInt32(4),
                            EnrolmentDate = reader.GetDateTime(5)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<Student> ReadAllStudents()
        {
            var students = new List<Student>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM Student", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentID = reader.GetString(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            EmailAddress = reader.GetString(3),
                            Age = reader.GetInt32(4),
                            EnrolmentDate = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return students;
        }
            public void UpdateStudent(Student student)
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqliteCommand(
                        "UPDATE Student SET FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, Age = @Age, EnrolmentDate = @EnrolmentDate WHERE StudentID = @StudentID",
                        connection);
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@EmailAddress", student.EmailAddress);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@EnrolmentDate", student.EnrolmentDate);
                    command.Parameters.AddWithValue("@StudentID", student.StudentID);
                    command.ExecuteNonQuery();
                }
            }

            public void DeleteStudent(string studentId)
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqliteCommand("DELETE FROM Student WHERE StudentID = @StudentID", connection);
                    command.Parameters.AddWithValue("@StudentID", studentId);
                    command.ExecuteNonQuery();
                }
            }
            public Student GetStudentById(string studentId)
        {
            Student student = null;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Students WHERE StudentID = @StudentID", connection);
                command.Parameters.AddWithValue("@StudentID", studentId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        student = new Student
                        {
                            StudentID = reader.GetString(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            EmailAddress = reader.GetString(3),
                            Age = reader.GetInt32(4)
                        };
                    }
                }
            }

            return student;
        }

                public List<Subject> GetSubjectsByStudentId(string studentId)
            {
                var subjects = new List<Subject>();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT s.SubjectID, s.Title FROM Subject s INNER JOIN Enrollment e ON s.SubjectID = e.SubjectID WHERE e.StudentID = @StudentID", connection);
                    command.Parameters.AddWithValue("@StudentID", studentId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
                            {
                                SubjectID = reader.GetString(0),
                                Title = reader.GetString(1)
                            });
                        }
                    }
                }

                return subjects;
            }
                }
}


    

