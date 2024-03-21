namespace RemoteEduApp.Models
{
    public class TeacherInfo
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public string? FullName { get; set; }
        public string? Mail { get; set; }
    }
}
