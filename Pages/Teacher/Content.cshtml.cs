using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Teacher
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

            string sql = "SELECT CourseId FROM RemoteEduDB.dbo.�ontent WHERE Id = " + ContentId;

            int CourseId = _dapper.LoadDataSingle<int>(sql);

            sql = "SELECT Courses.Id, Courses.SubjectName, Courses.SubjectShortName, Courses.Icon" +
                            " FROM [RemoteEduDB].[dbo].[Courses] JOIN [RemoteEduDB].[dbo].[Teacher_Courses] ON CourseId " +
                            "= Courses.Id JOIN [TeacherInfo] ON Teacher_Courses.TeacherId = TeacherInfo.Id WHERE TeacherInfo.Id ="
                            + User.FindFirst("Id").Value + " AND Courses.Id = " + CourseId + ";";

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
                sql = "SELECT TeacherInfo.FullName FROM �ontent JOIN TeacherInfo ON RemoteEduDB.dbo.�ontent.TeacherId = TeacherInfo.Id WHERE TeacherId = " + PageContent.TeacherId;
                Console.WriteLine(sql);
                TeacherName = _dapper.LoadDataSingle<string>(sql);
            }
            catch
            {
                TeacherName = "";
            }
        }
    }
}

