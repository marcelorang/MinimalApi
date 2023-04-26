using ApiCatalogo2.ApiEndpoints;
using ApiCatalogo2.AppServicesExtensions;
using ApiCatalogo2.Context;
using ApiCatalogo2.Models;
using ApiCatalogo2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();


var app = builder.Build();

app.MapAutenticacaoEndpoints();
app.MapcategoriasEndpoints();
app.MapProdutosEndpoints();

var enviroment = app.Environment;

app.UseExcepetionHandling(enviroment)
    .UseSwaggerMiddleware()
    .UseeAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

















