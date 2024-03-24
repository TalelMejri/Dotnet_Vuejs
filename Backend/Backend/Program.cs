
using Backend.Worflows;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRazorPages();
// Add Elsa services.

builder.Services.AddElsa(elsa =>
{

    elsa.UseWorkflowManagement(management => management.UseEntityFrameworkCore());

    elsa.UseWorkflowRuntime(runtime =>
    {
        runtime.UseEntityFrameworkCore();
    });
    
    elsa.UseIdentity(identity =>
    {
        identity.UseAdminUserProvider();
        identity.TokenOptions = tokenOptions => tokenOptions.SigningKey = "my-long-256-bit-secret-token-signing-key";
    });
    elsa.UseJavaScript();
    elsa.UseLiquid();
    elsa.UseWorkflows(workflows =>
    {
        // Configure workflow execution pipeline to handle workflow contexts.
        workflows.WithWorkflowExecutionPipeline(pipeline => pipeline
            .Reset()
            .UsePersistentVariables()
            .UseBookmarkPersistence()
            .UseWorkflowExecutionLogPersistence()
            .UseWorkflowExecutionLogPersistence()
            .UseActivityExecutionLogPersistence()
        );

        // Configure activity execution pipeline to handle workflow contexts.
        workflows.WithActivityExecutionPipeline(pipeline => pipeline
            .Reset()
            .UseBackgroundActivityInvoker()
        );
    });
    elsa.UseQuartz();
    elsa.UseScheduling(scheduling => scheduling.UseQuartzScheduler());
    elsa.UseWorkflowsApi(api => api.AddFastEndpointsAssembly<Program>());
    elsa.AddActivitiesFrom<Program>();
    elsa.UseHttp();
    elsa.UseDefaultAuthentication(auth => auth.UseAdminApiKey());
    elsa.UseRealTimeWorkflows();
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:8080")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();
builder.Services.AddControllersWithViews();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseCors("MyCorsPolicy");
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
