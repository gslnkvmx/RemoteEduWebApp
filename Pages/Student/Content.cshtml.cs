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

        public ContentModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
            string? ContentId = Request.Query["id"];
            string sql = "SELECT * FROM RemoteEduDB.dbo.Сontent WHERE Id = " + ContentId;
            PageContent = _dapper.LoadDataSingle<Content>(sql);

            if (PageContent == null)
            {
                ErrorMessage = "Здесь еще нет материала!";
            }
        }
    }
}
