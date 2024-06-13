using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using HolmesglenStudentManagementSystem.DAL;
using HolmesglenStudentManagementSystem.Models;
using Microsoft.Data.Sqlite;

namespace HolmesglenStudentManagementSystem.BLL
{
    public class EnrollmentBLL
    {
        private readonly EnrollmentDAL _enrollmentDAL;

        public EnrollmentBLL()
        {
            _enrollmentDAL = new EnrollmentDAL();
        }

        public void CreateEnrollment(Enrollment enrollment)
        {
            _enrollmentDAL.CreateEnrollment(enrollment);
        }

        public Enrollment GetEnrollmentById(int id)
        {
            return _enrollmentDAL.ReadEnrollment(id);
        }

        public List<Enrollment> GetAllEnrollments()
        {
            return _enrollmentDAL.ReadAllEnrollments();
        }

        public void UpdateEnrollment(Enrollment enrollment)
        {
            _enrollmentDAL.UpdateEnrollment(enrollment);
        }

        public void DeleteEnrollment(int id)
        {
            _enrollmentDAL.DeleteEnrollment(id);
        }
    
           public DataTable GetEnrollmentReport()
        {
            var dt = new DataTable();

            try
            {


                var connectionString = @"Data Source=/Users/wiley/Library/CloudStorage/OneDrive-HolmesglenInstitute/C#/AssessmentTask2/Project2/HolmesglenStudentManagementSystem/HolmesglenStudentManagementSystem.db;";        

                using var connection = new SqliteConnection(connectionString);
                connection.Open();
                var command = new SqliteCommand(
                    @"SELECT 
                            s.StudentID,
                            s.FirstName || ' ' || s.LastName AS StudentName,
                            sub.SubjectID,
                            sub.Title
                          FROM 
                            Enrollment e
                          JOIN 
                            Student s ON e.StudentID = s.StudentID
                          JOIN 
                            Subject sub ON e.SubjectID = sub.SubjectID;",
                    connection);
            }
            catch (ConstraintException ex)
            {
                Debug.WriteLine("ConstraintException: " + ex.Message);
                Debug.WriteLine("DataTable Rows:");
                foreach (DataRow row in dt.Rows)
                {
                    Debug.WriteLine(string.Join(", ", row.ItemArray));
                }
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
                throw;
            }

            return dt;
        }
    }
}
