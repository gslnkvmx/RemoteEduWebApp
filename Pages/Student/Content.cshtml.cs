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
                " JOIN [�ontent] ON Courses.Id = [�ontent].CourseId WHERE" +
                " (StudentInfo.Id = " + User.FindFirst("Id").Value + " AND [�ontent].Id = " + ContentId + ");";

            //Console.WriteLine(sql);
            try
            {
                int check = _dapper.LoadDataSingle<int>(sql);
            }
            catch (Exception ex)
            {
                ErrorMessage = "��� ���������� ���� ����!";
                return;
            }

            sql = "SELECT * FROM RemoteEduDB.dbo.�ontent WHERE Id = " + ContentId;

            try
            {
                PageContent = _dapper.LoadDataSingle<Content>(sql);
            }
            catch
            {
                ErrorMessage = "����� ��� ��� ���������!";
                return;
            }

            try
            {
                sql = "SELECT TeacherInfo.FullName FROM �ontent JOIN TeacherInfo ON RemoteEduDB.dbo.�ontent.TeacherId = TeacherInfo.Id WHERE TeacherId = " 
                    + PageContent.TeacherId + "AND RemoteEduDB.dbo.�ontent.Id = " + ContentId;
                TeacherName = _dapper.LoadDataSingle<string>(sql);
            }
            catch
            {
                TeacherName = "";
            }
        }
    }
}