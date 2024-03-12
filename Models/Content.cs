namespace RemoteEduApp.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Attachment { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfAdding { get; set; }
        public int CourseId { get; set; }

    }
}
