namespace HolmesglenStudentManagementSystem.Models
{
    public class Enrollment
    {
        public int ID { get; set; }
        public string StudentID { get; set; }
        public string SubjectID { get; set; }
        public object EnrolmentDate { get; internal set; }
    }
}