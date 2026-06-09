using Lesson3_CNLTWeb.Data;
using Lesson3_CNLTWeb.Middleware;
using Lesson3_CNLTWeb.Models;
using Lesson3_CNLTWeb.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookDbContext>();
    context.Database.EnsureCreated();

    if (!context.Books.Any())
    {
        context.Books.AddRange(
            new Book
            {
                Title = "Clean Code",
                Author = "Robert C. Martin",
                Price = 20,
                PublishDate = new DateTime(2008, 8, 1)
            },
            new Book
            {
                Title = "ASP.NET MVC",
                Author = "Microsoft Press",
                Price = 15,
                PublishDate = new DateTime(2012, 3, 15)
            },
            new Book
            {
                Title = "Design Pattern",
                Author = "Gang of Four",
                Price = 25,
                PublishDate = new DateTime(1994, 10, 21)
            });
        context.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
