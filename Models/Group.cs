namespace RemoteEduApp.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public IEnumerable<StudentInfo>? Students { get; set;}

    }
}
