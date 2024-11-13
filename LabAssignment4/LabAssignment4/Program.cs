using Microsoft.EntityFrameworkCore; 
using LabAssignment4.DataAccess; 
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StudentrecordContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("studentrecord")));

// Add services to the container.
builder.Services.AddRazorPages();
var dbConnStr = builder.Configuration.GetConnectionString("studentrecord");
builder.Services.AddDbContext<StudentrecordContext>(
    options => options.UseMySQL(dbConnStr));
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

app.UseAuthorization();

app.MapRazorPages();
//---
//where to og: http://localhost:5174/StudentManagement/Index
//---
app.Run();