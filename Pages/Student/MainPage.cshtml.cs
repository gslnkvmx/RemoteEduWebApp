using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Student
{
    [Authorize(Policy = "BelongToStudent")]
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
                " FROM [RemoteEduDB].[dbo].[Courses] JOIN [RemoteEduDB].[dbo].[Group_Courses] ON CourseId " +
                "= Courses.Id JOIN [StudentInfo] ON Group_Courses.GroupId = StudentInfo.GroupId WHERE StudentInfo.Id =" + User.FindFirst("Id").Value;
            CoursesList = _dapper.LoadData<Course>(sql);
        }
    }
}
