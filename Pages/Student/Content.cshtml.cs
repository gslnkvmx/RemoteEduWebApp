using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Student
{
    [Authorize(Policy = "BelongToStudent")]
    public class ContentModel : PageModel
    {
        string _errorMessage = "";
        public Content PageContent { get; set; }
        DataContextDapper _dapper;
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
        public string? TeacherName { get; set; }

        public ContentModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
            string? ContentId = Request.Query["id"];

            string sql = "SELECT Courses.Id" +
                " FROM [RemoteEduDB].[dbo].[Courses] JOIN [RemoteEduDB].[dbo].[Group_Courses] ON CourseId" +
                " = Courses.Id JOIN [StudentInfo] ON Group_Courses.GroupId = StudentInfo.GroupId " +
                " JOIN [Content] ON Courses.Id = [Content].CourseId WHERE" +
                " (StudentInfo.Id = " + User.FindFirst("Id").Value + " AND [Content].Id = " + ContentId + ");";

            //Console.WriteLine(sql);
            try
            {
                int check = _dapper.LoadDataSingle<int>(sql);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Вам недоступна эта страница!";
                return;
            }

            sql = "SELECT * FROM RemoteEduDB.dbo.Content WHERE Id = " + ContentId;

            try
            {
                PageContent = _dapper.LoadDataSingle<Content>(sql);
            }
            catch
            {
                ErrorMessage = "Здесь еще ничего нет!";
                return;
            }

            try
            {
                sql = "SELECT TeacherInfo.FullName FROM Content JOIN TeacherInfo ON RemoteEduDB.dbo.Content.TeacherId = TeacherInfo.Id WHERE TeacherId = "
                    + PageContent.TeacherId + "AND RemoteEduDB.dbo.Content.Id = " + ContentId;
                TeacherName = _dapper.LoadDataSingle<string>(sql);
            }
            catch
            {
                TeacherName = "";
            }
        }
    }
}