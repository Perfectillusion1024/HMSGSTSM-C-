using HolmesglenStudentManagementSystem.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Data.SqlClient;
namespace HolmesglenStudentManagementSystem.DAL
{
    public class EnrollmentDAL
    {
private string connectionString = @"Data Source=/Users/wiley/Library/CloudStorage/OneDrive-HolmesglenInstitute/C#/AssessmentTask2/Project2/HolmesglenStudentManagementSystem/HolmesglenStudentManagementSystem.db;";
       public void CreateEnrollment(Enrollment enrollment)
        {

            using var connection = new SqliteConnection(connectionString); 
            connection.Open();
            var command = new SqliteCommand(
                "INSERT INTO Enrollment (StudentID, SubjectID) VALUES (@StudentID, @SubjectID)",
                connection);
            command.Parameters.AddWithValue("@StudentID", enrollment.StudentID);
            command.Parameters.AddWithValue("@SubjectID", enrollment.SubjectID);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqliteException ex)
            {
                
                Console.WriteLine($"SQLite Error: {ex.ErrorCode}: {ex.Message}");
            }
        }

        public Enrollment ReadEnrollment(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM Enrollment WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Enrollment
                        {
                            ID = reader.GetInt32(0),
                            StudentID = reader.GetString(1),
                            SubjectID = reader.GetString(2)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<Enrollment> ReadAllEnrollments()
        {
            var enrollments = new List<Enrollment>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM Enrollment", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        enrollments.Add(new Enrollment
                        {
                            ID = reader.GetInt32(0),
                            StudentID = reader.GetString(1),
                            SubjectID = reader.GetString(2)
                        });
                    }
                }
            }
            return enrollments;
        }

        public void UpdateEnrollment(Enrollment enrollment)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand(
                    "UPDATE Enrollment SET StudentID = @StudentID, SubjectID = @SubjectID WHERE ID = @ID",
                    connection);
                command.Parameters.AddWithValue("@StudentID", enrollment.StudentID);
                command.Parameters.AddWithValue("@SubjectID", enrollment.SubjectID);
                command.Parameters.AddWithValue("@ID", enrollment.ID);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEnrollment(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand("DELETE FROM Enrollment WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }
                public void SendEnrollmentEmail(string studentId, string connectionString)
                {
                    Student student;
                    List<Subject> subjects = new List<Subject>();
                
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                
                        // Get student information
                        var studentCommand = new SqlCommand("SELECT FirstName FROM Student WHERE StudentID = @StudentID", connection);
                        studentCommand.Parameters.AddWithValue("@StudentID", studentId);
                
                        using (var reader = studentCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                student = new Student
                                {
                                    FirstName = reader.GetString(0)
                                };
                            }
                            else
                            {
                                throw new Exception("Student not found");
                            }
                        }
                
                        // Get subjects information
                        var subjectsCommand = new SqlCommand("SELECT s.SubjectID, s.Title FROM Subject s INNER JOIN Enrollment e ON s.SubjectID = e.SubjectID WHERE e.StudentID = @StudentID", connection);
                        subjectsCommand.Parameters.AddWithValue("@StudentID", studentId);
                
                        using (var reader = subjectsCommand.ExecuteReader())
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
                
                    // Generate and print the email
                    string email = GenerateEnrollmentEmail(student, subjects);
                    Console.WriteLine(email);
                }
                
                public string GenerateEnrollmentEmail(Student student, List<Subject> subjects)
                {
                    // Generate the email content
                    string emailContent = $"Dear {student.FirstName},\n\nYou have been enrolled in the following subjects:\n";
                    foreach (var subject in subjects)
                    {
                        emailContent += $"{subject.SubjectID} - {subject.Title}\n";
                    }
                    emailContent += "\nBest regards,\nThe Enrollment Team";
                
                    return emailContent;
                }


    }
}