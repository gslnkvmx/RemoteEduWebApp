using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Student
{
    [Authorize(Policy = "BelongToStudent")]
    public class CourseModel : PageModel
    {
        string _errorMessage = "";
        public IEnumerable<Content> ContentList { get; set; }
        DataContextDapper _dapper;
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }

        public CourseModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
            string? CourseId = Request.Query["course"];

            string sql = "SELECT Courses.Id" +
                " FROM [RemoteEduDB].[dbo].[Courses] JOIN [RemoteEduDB].[dbo].[Group_Courses] ON CourseId" +
                " = Courses.Id JOIN [StudentInfo] ON Group_Courses.GroupId = StudentInfo.GroupId WHERE" +
                " (StudentInfo.Id = " + User.FindFirst("Id").Value + " AND Courses.Id = " + CourseId + ");";

            //Console.WriteLine(sql);
            try
            {
                int check = _dapper.LoadDataSingle<int>(sql);
            }
            catch (Exception ex) {
                ErrorMessage = "Вам недоступен этот курс!";
                return;
            }

            sql = "SELECT * FROM [RemoteEduDB].[dbo].[Сontent] WHERE CourseId = " + CourseId + " ORDER BY DateOfAdding DESC";
            ContentList = _dapper.LoadData<Content>(sql);

            if(ContentList.Count() == 0) 
            {
                ErrorMessage = "Здесь еще нет материала!";
                return;
            }
            return;
        }
    }
}
