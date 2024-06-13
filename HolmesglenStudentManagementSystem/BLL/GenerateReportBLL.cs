using System.Collections.Generic;
using System.Data;
using HolmesglenStudentManagementSystem.DAL;
using HolmesglenStudentManagementSystem.Models;
using Microsoft.Data.Sqlite;

namespace HolmesglenStudentManagementSystem.BLL
{
    public class GenerateReportBLL
    {
        public List<ReportModel> GetAll()
        {
            SqliteConnection connection = new SqliteConnection(@"Data Source=/Users/wiley/Library/CloudStorage/OneDrive-HolmesglenInstitute/C#/AssessmentTask2/Project2/HolmesglenStudentManagementSystem/HolmesglenStudentManagementSystem.db;");
            GenerateReportDAL generateReportDAL = new GenerateReportDAL(connection);
            return generateReportDAL.ReadAll();
        }

        internal bool Run()
        {
            throw new NotImplementedException();
        }
    }
    
}