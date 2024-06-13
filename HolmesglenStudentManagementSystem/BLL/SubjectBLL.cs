using System.Collections.Generic;
using HolmesglenStudentManagementSystem.DAL;
using HolmesglenStudentManagementSystem.Models;

namespace HolmesglenStudentManagementSystem.BLL
{
    public class SubjectBLL
    {
        private readonly SubjectDAL _subjectDAL;

        public SubjectBLL()
        {
            _subjectDAL = new SubjectDAL();
        }

        public void CreateSubject(Subject subject)
        {
            _subjectDAL.CreateSubject(subject);
        }

        public Subject GetSubjectById(string subjectId)
        {
            return _subjectDAL.ReadSubject(subjectId);
        }

        public List<Subject> GetAllSubjects()
        {
            return _subjectDAL.ReadAllSubjects();
        }

        public void UpdateSubject(Subject subject)
        {
            _subjectDAL.UpdateSubject(subject);
        }

        public void DeleteSubject(string subjectId)
        {
            _subjectDAL.DeleteSubject(subjectId);
        }
    }
}