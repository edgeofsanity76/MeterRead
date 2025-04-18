using MeterRead.Api.Endpoints;
using MeterRead.Api.ExceptionHandler;
using MeterRead.Application.Interfaces;
using MeterRead.Application.Services;
using MeterRead.Data;
using MeterRead.Data.Context;
using MeterRead.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite<MeterReadDbContext>("DataSource=file:MeterReadDb.db;Mode=ReadWrite");
builder.Services.AddScoped<IMeterDataService, MeterDataService>();
builder.Services.AddScoped<IMetaDataParserService, MeterDataParserService>();
builder.Services.AddScoped<IMeterValidationService, MeterDataValidationService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseHttpsRedirection();
app.UseExceptionHandler("/error");

Meter.Map(app);
Ping.Map(app);

app.Run();
