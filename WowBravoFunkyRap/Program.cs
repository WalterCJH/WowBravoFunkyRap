using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nelibur.ObjectMapper;
using NLog;
using NLog.Web;
using OfficeOpenXml;
using WowBravoFunkyRap.ActionFilter;
using WowBravoFunkyRap.Areas.Manage.ViewModels.PublicityImages;
using WowBravoFunkyRap.Model;
using WowBravoFunkyRap.Model.Tables;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");


try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllersWithViews();

    builder.Logging.ClearProviders(); // 將NLog註冊到此專案內
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information); // 設定log紀錄的最小等級
    builder.Host.UseNLog();

    var configuration = builder.Configuration;

    builder.Services.AddService(configuration);

    builder.Services.AddDbContext<WowBravoFunkyRapDbContext>(options =>
        options.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 25))));


    // 設置基於Cookie的身份驗證方案為默認方案
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            //options.Cookie.Name = "YourAppName.Identity";
            options.Cookie.HttpOnly = true;
            //options.Cookie.SecurePolicy = CookieSecurePolicy.Secure;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(1); // 身份驗證 cookie 的過期時間
            options.LoginPath = "/Accounts/Login";
            options.LogoutPath = "/Accounts/Logout";
            options.AccessDeniedPath = "/Accounts/AccessDenied";

            //options.ReturnUrlParameter = "returnUrl"; // 用於保持原始請求URL的查詢字符串參數的名稱
            //options.SlidingExpiration = true; // 是否使用滑動的過期時間

            // 事件設定
            //options.Events.OnRedirectToLogin = (context) =>
            //{
            //    // 如果是API請求則返回401，而不是重定向
            //    if (context.Request.Path.StartsWithSegments("/api")
            //        && context.Response.StatusCode == (int)HttpStatusCode.OK)
            //    {
            //        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //    }
            //    else
            //    {
            //        context.Response.Redirect(context.RedirectUri);
            //    }
            //    return Task.CompletedTask;
            //};
        });

    builder.Services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));

    //builder.Services.AddDistributedMemoryCache();  // 使用內存作為分佈式緩存。在生產環境中，您可能想使用 Redis 或 SQL Server
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(240);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });
    builder.Services.AddSharedService(configuration);
    builder.Services.AddScoped<MenuActionFilter>();

    // Add services to the container.
    builder.Services.AddControllersWithViews();


    TinyMapper.Bind<PublicityImage, PublicityImageCommon>();
    TinyMapper.Bind<PublicityImageCommon, PublicityImage>(config =>
    {
        config.Ignore(x => x.CreateUserId);
        config.Ignore(x => x.CreateTime);
        config.Ignore(x => x.UpdateUserId);
        config.Ignore(x => x.UpdateTime);
    });

    builder.Services.AddRazorPages();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSession();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });

    //app.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");

    //app.UseEndpoints(endpoints =>
    //{
    //    endpoints.MapControllerRoute(
    //      name: "areas",
    //      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    //    );
    //});

    app.Run();

}
catch (Exception ex)
{
    // 捕獲設定錯誤的錯誤紀錄
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    //須確定在關閉時，把nlog關閉
    LogManager.Shutdown();
}