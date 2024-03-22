using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Student
{
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
                " JOIN [Сontent] ON Courses.Id = [Сontent].CourseId WHERE" +
                " (StudentInfo.Id = " + User.FindFirst("Id").Value + " AND [Сontent].Id = " + ContentId + ");";

            //Console.WriteLine(sql);
            try
            {
                int check = _dapper.LoadDataSingle<int>(sql);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Вам недоступен этот курс!";
                return;
            }

            sql = "SELECT * FROM RemoteEduDB.dbo.Сontent WHERE Id = " + ContentId;

            try
            {
                PageContent = _dapper.LoadDataSingle<Content>(sql);
            }
            catch
            {
                ErrorMessage = "Здесь еще нет материала!";
                return;
            }

            try
            {
                sql = "SELECT TeacherInfo.FullName FROM Сontent JOIN TeacherInfo ON RemoteEduDB.dbo.Сontent.TeacherId = TeacherInfo.Id WHERE TeacherId = " 
                    + PageContent.TeacherId + "AND RemoteEduDB.dbo.Сontent.Id = " + ContentId;
                TeacherName = _dapper.LoadDataSingle<string>(sql);
            }
            catch
            {
                TeacherName = "";
            }
        }
    }
}