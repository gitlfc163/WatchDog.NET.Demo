
using CAP.Transport.RabbitMQ.MySql.Models;
using WatchDog;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);

var appSetting = new AppSetting();
builder.Configuration.GetSection("AppSetting").Bind(appSetting);

#region WatchDog
//builder.Services.AddWatchDogServices();

//����ʹ��MySql��ʽ
//builder.Services.AddWatchDogServices(opt =>
//{
//    opt.IsAutoClear = false;
//    opt.SetExternalDbConnString = appSetting.MysqlSetting.Connection;
//    opt.SqlDriverOption = WatchDogSqlDriverEnum.MySql;
//});

//����ʹ�ñ������ݿⷽʽ
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

//��ӿ��Ź��쳣��¼��
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

//���� WatchDog �м��
app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "admin";
    opt.WatchPagePassword = "Qwerty@123";
});

app.Run();
