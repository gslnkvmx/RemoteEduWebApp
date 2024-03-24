namespace RemoteEduApp.Models
{
    public class StudentInfo
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public string? FullName { get; set; }
        public int GroupId { get; set; }
        public int Year { get; set; }
        public string? Mail { get; set; }

    }
}
