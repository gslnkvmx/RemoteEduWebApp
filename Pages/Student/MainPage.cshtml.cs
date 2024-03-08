using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Student
{

    public class MainPageModel : PageModel
    {

        public IEnumerable<Courses> CoursesList { get; set; }
        DataContextDapper _dapper;

        public MainPageModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
            string sql = "select * from courses;";
            CoursesList = _dapper.LoadData<Courses>(sql);
        }
    }
}
