using System.Collections.Generic;
using System.Data.SqlClient;
using HolmesglenStudentManagementSystem.DAL;
using HolmesglenStudentManagementSystem.Models;

namespace HolmesglenStudentManagementSystem.BLL
{
    public class StudentBLL
    {
        private readonly StudentDAL _studentDAL;

        public StudentBLL()
        {
            _studentDAL = new StudentDAL();
        }

        public void CreateStudent(Student student)
        {
            _studentDAL.CreateStudent(student);
        }


        public List<Student> GetAllStudents()
        {
            return _studentDAL.ReadAllStudents();
        }

        public void UpdateStudent(Student student)
        {
            _studentDAL.UpdateStudent(student);
        }

        public void DeleteStudent(string studentId)
        {
            _studentDAL.DeleteStudent(studentId);
        }


     
        public Student GetStudentById(string studentId)
        {
            return _studentDAL.GetStudentById(studentId);
        }

        public List<Subject> GetSubjectsByStudentId(string studentId)
        {
            return _studentDAL.GetSubjectsByStudentId(studentId);
        }
    
}
}
