
using CAP.Transport.RabbitMQ.MySql.Models;
using WatchDog;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);

var appSetting = new AppSetting();
builder.Configuration.GetSection("AppSetting").Bind(appSetting);

#region WatchDog
//builder.Services.AddWatchDogServices();

//配置使用MySql方式
//builder.Services.AddWatchDogServices(opt =>
//{
//    opt.IsAutoClear = false;
//    opt.SetExternalDbConnString = appSetting.MysqlSetting.Connection;
//    opt.SqlDriverOption = WatchDogSqlDriverEnum.MySql;
//});

//配置使用本地数据库方式
builder.Services.AddWatchDogServices(opt =>
{
    opt.IsAutoClear = false;
}); 
#endregion

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//添加看门狗异常记录器
app.UseWatchDogExceptionLogger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//配置 WatchDog 中间件
app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "admin";
    opt.WatchPagePassword = "Qwerty@123";
});

app.Run();
