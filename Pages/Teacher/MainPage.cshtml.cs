using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Teacher
{
    [Authorize(Policy = "BelongToTeacher")]
    public class MainPageModel : PageModel
    {

        public IEnumerable<Course> CoursesList { get; set; }
        DataContextDapper _dapper;

        public MainPageModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
            string sql = "SELECT Courses.Id, Courses.SubjectName, Courses.SubjectShortName, Courses.Icon" +
                " FROM [RemoteEduDB].[dbo].[Courses] JOIN [RemoteEduDB].[dbo].[Teacher_Courses] ON CourseId " +
                "= Courses.Id JOIN [TeacherInfo] ON Teacher_Courses.TeacherId = TeacherInfo.Id WHERE TeacherInfo.Id =" + User.FindFirst("Id").Value;
            CoursesList = _dapper.LoadData<Course>(sql);
        }
    }
}
