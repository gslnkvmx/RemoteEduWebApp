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
            string sql = "EXECUTE [dbo].[SelectTeacherCourses] N'" + User.FindFirst("Id").Value + "';";

            CoursesList = _dapper.LoadData<CourseWithGroup>(sql);
        }
    }
}
