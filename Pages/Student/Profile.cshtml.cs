using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Student
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        string _errorMessage = "";
        public StudentInfo StudentProfile { get; set; }
        DataContextDapper _dapper;
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }

        public ProfileModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
            string? studentId = Request.Query["id"];
            string sql = "SELECT * FROM RemoteEduDB.dbo.StudentInfo WHERE Id = " + studentId;
            StudentProfile = _dapper.LoadDataSingle<StudentInfo>(sql);

            if (StudentProfile == null)
            {
                ErrorMessage = "Здесь еще нет материала!";
            }
        }
    }
}
