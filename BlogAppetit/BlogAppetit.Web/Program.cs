using BlogAppetit.Web.Data;
using BlogAppetit.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogAppetitDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAppetitDbConnectionString")));

builder.Services.AddDbContext<LoginDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAppetitLoginDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<LoginDbContext>(); // injetemos o IdentityUser e IdentityRole na nossa db

//DEPENDENCY INJECTIONS
builder.Services.AddScoped<ITagRepository, TagRepository>(); //quando alguem chama o ITagRepository interface, vai ser instanciado o TagRepository, dependency injection
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>(); // quando alguem chama o IBlogPostRepository interface, vai ser instanciado o BlogPostRepository, dependency injection
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>(); // quando alguem chama o IImageRepository interface, vai ser instanciado o CloudinaryImageRepository, dependency injection
builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>(); // quando alguem chama o IBlogPostLikeRepository interface, vai ser instanciado o BlogPostLikeRepository, dependency injection
builder.Services.AddScoped<IUserRepository, UserRepository>(); // quando alguem chama o IUserRepository interface, vai ser instanciado o UserRepository, dependency injection   
builder.Services.AddScoped<IBlogPostCommentRepository, BlogPostCommentRepository>(); // quando alguem chama o IBlogPostCommentRepository interface, vai ser instanciado o BlogPostCommentRepository, dependency injection
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //linha importante e fundamental, middleware do asp.net core que permite a autenticação

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
