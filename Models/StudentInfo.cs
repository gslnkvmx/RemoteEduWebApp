namespace RemoteEduApp.Models
{
    public class StudentInfo
    {
        public required string Login { get; set; }
        public string? FullName { get; set; }
        public string? Group { get; set; }
        public int Year { get; set; }
        public string? Mail { get; set; }

    }
}
