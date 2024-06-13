using HolmesglenStudentManagementSystem.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace HolmesglenStudentManagementSystem.DAL
{
    public class GenerateReportDAL
    {
        private string connectionString = @"Data Source=C:\Users\623209788\OneDrive - Holmesglen Institute\C#\AssessmentTask2\Project2\HolmesglenStudentManagementSystem\HolmesglenStudentManagementSystem.db;";
        private SqliteConnection _connection;

        public GenerateReportDAL(SqliteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public static object ReportDALInstance { get; internal set; }

        public List<ReportModel> ReadAll()
        {
            var report = new List<ReportModel>();
            _connection.Open();

            var command = _connection.CreateCommand();
            command.CommandText = @"
                SELECT Student.StudentID, Student.FirstName, Student.LastName, Subject.Title, Subject.SubjectID
                FROM Student
                JOIN Enrollment ON Student.StudentID = Enrollment.StudentID
                JOIN Subject ON Enrollment.SubjectID = Subject.SubjectID
                ORDER BY Student.StudentID
            ";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var studentID = reader.GetString(0);
                    var studentFName = reader.GetString(1);
                    var studentLName = reader.GetString(2);
                    var subjectTitle = reader.GetString(3);
                    var subjectID = reader.GetString(4);
                    report.Add(new ReportModel(studentID, studentFName, studentLName, subjectTitle, subjectID));
                }
            }

            _connection.Close();
            return report;
        }
    }
}
