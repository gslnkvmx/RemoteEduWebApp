using System.ComponentModel.DataAnnotations;

namespace RemoteEduApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty ;

        public string Role { get; set; } = string.Empty;
    }
}
