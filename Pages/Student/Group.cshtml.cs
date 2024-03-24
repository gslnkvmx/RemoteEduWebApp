using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;
using System.Text.RegularExpressions;
using Group = RemoteEduApp.Models.Group;

namespace RemoteEduApp.Pages.Student
{
    public class GroupModel : PageModel
    {
        string _errorMessage = "";
        public Group? PageGroup { get; set; }
        DataContextDapper _dapper;
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }

        public GroupModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
            string? groupId = Request.Query["id"];
            string sql = "SELECT [Group].Id, [Group].Name FROM [Group] WHERE Id = " + groupId;

            try
            {
               PageGroup = _dapper.LoadDataSingle<Group>(sql);

                sql = "StudentInfo.Id, StudentInfo.[Login], StudentInfo.FullName, StudentInfo.GroupId, StudentInfo.[Year], StudentInfo.Mail" +
                    " FROM StudentInfo WHERE StudentInfo.GroupId = " + groupId;

                PageGroup.Students = _dapper.LoadData<StudentInfo>(sql);

            }
            catch (Exception ex)
            {
                ErrorMessage = "Группа не найдена!";
            }

            Console.WriteLine(PageGroup.Name);

            foreach(var student in PageGroup.Students)
            {
                Console.WriteLine("STUIDENT:"+student.FullName);
            }
        }
    }
}
