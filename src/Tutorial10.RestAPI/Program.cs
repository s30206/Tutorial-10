using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Tutorial10.RestAPI;
using Tutorial10.RestAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<SampleCompanyContext>(o => o.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/jobs", async (SampleCompanyContext context, CancellationToken token) => {
    try
    {
        return Results.Ok(await context.Jobs.ToListAsync(token));
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/departments", async (SampleCompanyContext context, CancellationToken token) => {
    try
    {
        return Results.Ok(await context.Departemnts.ToListAsync(token));
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/employees", async (SampleCompanyContext context, CancellationToken token) =>
{
    try
    {
        var employees = await context.Employees.ToListAsync(token);
        var result = new List<EmployeeDTO>();

        foreach (var employee in employees)
        {
            result.Add(new EmployeeDTO(employee));
        }
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/employees/{id}", async (SampleCompanyContext context, CancellationToken token, int id) =>
{
    try
    {
        var employee = await context.Employees.FindAsync(id, token);
        return employee is null ? Results.NotFound() : Results.Ok(new EmployeeDTO(employee));
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("/api/employees", () =>
{
    try
    {
        throw new NotImplementedException();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPut("/api/employees/{id}", (int id) =>
{
    try
    {
        throw new NotImplementedException();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapDelete("/api/employees/{id}", async (SampleCompanyContext context, CancellationToken token, int id) =>
{
    try
    {
        throw new NotImplementedException();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();
