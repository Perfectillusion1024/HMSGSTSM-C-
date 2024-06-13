namespace HolmesglenStudentManagementSystem.Models
{
    public class Student
    {
        public string StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public DateTime EnrolmentDate { get; set; }
    }
    
}