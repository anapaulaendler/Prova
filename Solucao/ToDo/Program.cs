using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "API for creating tasks!");

app.MapPost("/api/create", ([FromBody] Activity a, [FromServices] AppDataContext ctx) =>
{
    ctx.TableOfActivities.Add(a);
    ctx.SaveChanges();
    return Results.Ok(a);
});

app.MapPost("/api/create/list", ([FromBody] List<Activity> a, [FromServices] AppDataContext ctx) =>
{
    ctx.TableOfActivities.AddRange(a);
    ctx.SaveChanges();
    return Results.Ok();
});

app.MapGet("/api/list", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TableOfActivities.Any())
    {
        List<Activity> a = ctx.TableOfActivities.ToList();
        return Results.Ok(a);
    }

    return Results.NotFound();
});

app.MapPut("/api/edit/{id}", ([FromRoute] string id, [FromBody] Activity aAltered, [FromServices] AppDataContext ctx) =>
{
    Activity? a = ctx.TableOfActivities.Find(id);
    
    if (a is null)
    {
        return Results.NotFound();
    }

    a.Description = aAltered.Description;
    a.LimitDate = aAltered.LimitDate;
    a.Name = aAltered.Name;
    a.Status = aAltered.Status;

    ctx.TableOfActivities.Update(a);
    ctx.SaveChanges();
    return Results.Ok(a);
});

app.MapGet("/api/find/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Activity? a = ctx.TableOfActivities.Find(id);
    
    if (a is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(a);
});

app.MapDelete("/api/delete/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Activity? a = ctx.TableOfActivities.Find(id);
    
    if (a is null)
    {
        return Results.NotFound();
    }

    ctx.TableOfActivities.Remove(a);
    ctx.SaveChanges();
    return Results.Ok();
});

app.Run();
