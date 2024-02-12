using Backend.Worflows;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.Expressions.Models;
using Elsa.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Add Elsa services.
builder.Services.AddElsa(elsa =>
{
    elsa.UseIdentity(identity =>
    {
        identity.UseAdminUserProvider();
        identity.TokenOptions = tokenOptions => tokenOptions.SigningKey = "my-long-256-bit-secret-token-signing-key";
    });
    elsa.UseDefaultAuthentication();
    elsa.UseWorkflowManagement(management => management.UseEntityFrameworkCore());
    elsa.UseWorkflowRuntime(runtime => runtime.UseEntityFrameworkCore());

    elsa.UseJavaScript();
    elsa.UseLiquid();
    elsa.UseWorkflowsApi();
    elsa.UseHttp(http => http.ConfigureHttpOptions = options =>
    {
        options.BaseUrl = new Uri("http://localhost:5182");
        options.BasePath = "/workflows";
    });
    elsa.AddWorkflowsFrom<Program>();
    elsa.AddActivitiesFrom<Program>();
    elsa.UseHttp();
});

var app = builder.Build();
builder.Services.AddControllersWithViews();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseRouting();

app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseWorkflowsApi();
app.UseWorkflows();
app.MapControllers();
app.MapRazorPages();
app.Run();




