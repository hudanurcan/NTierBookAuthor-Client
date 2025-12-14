using Project.Bll.DependencyResolvers;
using Project.WebApi.MapperResolvers;
using FluentValidation;
using Validators.ModelsValidators;
using Validators.RequestValidator.Authors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddValidatorsFromAssemblyContaining<CategoryDtoValidator>(); // birini eklemek yetiyor. diğer hepsini de bunun sayesinde ekler
builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorRequestModelValidator>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextService(); //Context class'ının middleware'e eklenmesi
builder.Services.AddRepositoryService(); //Repository servisinin middleware'e eklenmesi
builder.Services.AddManagerService(); //Manager servisinin middleware'e eklenmesi
builder.Services.AddDtoMapperService(); //Dto mapper servisinin middleware'e eklenmesi
builder.Services.AddVmMapperService(); //Vm Mapper servisinin middleware'e eklenmesi

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.MapControllers();

app.Run();
