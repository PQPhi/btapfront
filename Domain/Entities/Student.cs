namespace Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }

        // Navigation property
        public Class? Class { get; set; }
    }
}
