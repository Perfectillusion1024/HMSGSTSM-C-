namespace HolmesglenStudentManagementSystem.Models
{
    
    public class ReportModel
    {
        public string StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SubjectTitle { get; set; }
        public string SubjectID { get; set; }

        public ReportModel()
        {
            StudentID = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            SubjectTitle = string.Empty;
            SubjectID = string.Empty;
        }

        public ReportModel(string id, string firstName, string lastName, string subjectTitle, string subjectID)
        {
            StudentID = id;
            FirstName = firstName;
            LastName = lastName;
            SubjectTitle = subjectTitle;
            SubjectID = subjectID;
        }
    }
}