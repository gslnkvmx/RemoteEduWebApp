namespace RemoteEduApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services
                    .AddRazorPages()
                    .AddRazorPagesOptions(opt => opt.Conventions.AddPageRoute("/Account/Login", ""));

            builder.Services.AddAuthentication("AuthCookie").AddCookie("AuthCookie", options =>
            {
                options.Cookie.Name = "AuthCookie";
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("BelongToStudent", policy => policy.RequireClaim("Role", "Student"));
                options.AddPolicy("BelongToTeacher", policy => policy.RequireClaim("Role", "Teacher"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
