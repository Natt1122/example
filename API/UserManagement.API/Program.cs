using Microsoft.EntityFrameworkCore;
using UserManage.BLL.service.contrato;
using UserManage.BLL.service;
using UserManage.DAL.DBContext;
using UserManage.DAL.Repos.Contrato;
using UserManage.DAL.Repos;
using UserManage.Utility;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
} );





   
        builder.Services.AddDbContext<UserManageContext>(options => {
            options.UseSqlServer(builder.Configuration.GetConnectionString("conexion"));
        });

        builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        builder.Services.AddScoped<IRolService, RolService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IMenuService, MenuService>();

    


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
