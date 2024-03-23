using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.DTOs;
using System.Text.RegularExpressions;

namespace RemoteEduApp.Pages.Teacher
{
    [Authorize(Policy = "BelongToTeacher")]
    public class MainPageModel : PageModel
    {

        public IEnumerable<CourseWithGroup> CoursesList { get; set; }

        DataContextDapper _dapper;

        public MainPageModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
            string sql = "SELECT Courses.Id, Courses.SubjectName, Courses.SubjectShortName, Courses.Icon, [RemoteEduDB].[dbo].[Group].[Name]" +
                " FROM [RemoteEduDB].[dbo].[Courses]" +
                " JOIN [RemoteEduDB].[dbo].[Teacher_Courses] ON CourseId = Courses.Id" +
                " JOIN [TeacherInfo] ON Teacher_Courses.TeacherId = TeacherInfo.Id" +
                " JOIN [Group_Courses] ON [Courses].Id = Group_Courses.CourseId" +
                " JOIN [Group] ON GroupId = Group_Courses.GroupId" +
                " WHERE TeacherInfo.Id = " + User.FindFirst("Id").Value;

            CoursesList = _dapper.LoadData<CourseWithGroup>(sql);
        }
    }
}
