using MeterRead.Api;
using MeterRead.Api.Endpoints;
using MeterRead.Application.Interfaces;
using MeterRead.Application.Services;
using MeterRead.Data;
using MeterRead.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite<MeterReadDbContext>("DataSource=file:MeterReadDb.db;Mode=ReadWrite");
builder.Services.AddScoped<IMeterDataService, MeterDataService>();
builder.Services.AddScoped<IMetaDataParserService, MeterDataParserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler("/error");

Meter.Map(app);

app.Run();
