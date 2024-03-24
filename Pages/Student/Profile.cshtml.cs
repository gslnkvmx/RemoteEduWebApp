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
        public string groupName;
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

            try
            {
                StudentProfile = _dapper.LoadDataSingle<StudentInfo>(sql);
                sql = "SELECT [Group].Name FROM [RemoteEduDB].[dbo].[StudentInfo] JOIN [Group] ON GroupId = [Group].[Id] WHERE StudentInfo.Id = " + studentId;

                groupName = _dapper.LoadDataSingle<string>(sql);
            }
            catch (Exception ex)
            {
                    ErrorMessage = "Пользователь не найден!";
            }
        }
    }
}
