using _3ecexamen.Data;
using _3ecexamen.Repositories;
using _3ecexamen.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

//TODO 
builder.Services.AddScoped<IConferenceRepository,IConferenceRepository>();
builder.Services.AddScoped<IRoomRepository,IRoomRepository>();
builder.Services.AddScoped<ISpeakerRepository,ISpeakerRepository>();
builder.Services.AddScoped<IConferenceService,IConferenceService>();
builder.Services.AddScoped<ISpeakerService,ISpeakerService>();
builder.Services.AddScoped<ITalkRepository, ITalkRepository>();
builder.Services.AddScoped<ITalkService,ITalkService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
