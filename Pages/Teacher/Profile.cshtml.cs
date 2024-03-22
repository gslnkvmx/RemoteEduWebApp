using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Teacher
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        string _errorMessage = "";
        public TeacherInfo TeacherProfile { get; set; }
        DataContextDapper _dapper;
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }

        public ProfileModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
            string? teacherId = Request.Query["id"];
            string sql = "SELECT * FROM RemoteEduDB.dbo.TeacherInfo WHERE Id = " + teacherId;

            try
            {
                TeacherProfile = _dapper.LoadDataSingle<TeacherInfo>(sql);

                sql = "SELECT [Group].Name FROM [RemoteEduDB].[dbo].[TeacherInfo] JOIN [Group] ON GroupId = [Group].[Id] WHERE TeacherInfo.Id = " + teacherId;
            }
            catch (Exception ex)
            {
                _errorMessage = "Пользователь не найден!";
            }
        }
    }
}
