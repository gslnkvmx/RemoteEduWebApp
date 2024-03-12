using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Student
{
    public class CourseModel : PageModel
    {
        string _errorMessage = "";
        public IEnumerable<Content> ContentList { get; set; }
        DataContextDapper _dapper;
        public string CourseName { get; set; }
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }

        public CourseModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
            string? CourseId = Request.Query["course"];
            string sql = "SELECT * FROM RemoteEduDB.dbo.Сontent WHERE CourseId = " + CourseId;
            ContentList = _dapper.LoadData<Content>(sql);

            if(ContentList.Count() == 0) 
            {
                ErrorMessage = "Здесь еще нет материала!";
            }

            string selectCourseName = "SELECT [SubjectShortName] FROM [RemoteEduDB].[dbo].[Courses] WHERE Id = " + CourseId;

            CourseName = _dapper.LoadDataSingle<string>(selectCourseName);
        }
    }
}
