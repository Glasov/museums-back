using WebApplication2.Data;
using WebApplication2.Data.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<EducationContext>();
builder.Services.AddTransient<UsersService>();
builder.Services.AddTransient<MuseumsService>();
builder.Services.AddTransient<ExhibitionService>();
builder.Services.AddTransient<ExhibitionItemsService>();
builder.Services.AddTransient<OrdersService>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.Run();
