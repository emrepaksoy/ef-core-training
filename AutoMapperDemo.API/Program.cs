using AutoMapperDemo.API.Context;
using AutoMapperDemo.API.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AutoMapDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddSingleton<IFoo, Foo>();
builder.Services.AddSingleton<IBar, Bar>();

//builder.Services.AddScoped<IFoo,Foo>();
//builder.Services.AddScoped<IBar,Bar>();

//builder.Services.AddTransient<IFoo,Foo>();
//builder.Services.AddTransient<IBar,Bar>();

builder.Services.AddScoped<DbContext>(x => x.GetService<AutoMapDbContext>());

//var columnOptions = new ColumnOptions();
//columnOptions.Store.Remove(StandardColumn.Properties);



//var columnOptions = new ColumnOptions
//{
//    AdditionalColumns = new Collection<SqlColumn>
//    {
//        new SqlColumn
//            {ColumnName = "EnvironmentUserName", PropertyName = "UserName", DataType = SqlDbType.NVarChar, DataLength = 64},

//        new SqlColumn
//            {ColumnName = "UserId", DataType = SqlDbType.BigInt, NonClusteredIndex = true},

//        new SqlColumn
//            {ColumnName = "RequestUri", DataType = SqlDbType.NVarChar, DataLength = -1, AllowNull = false},
//    }
//};


Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),"Logs", 
        autoCreateSqlTable: true
        ) 
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
