using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();


app.MapGet("/", () => "Aplicativo de Cadastro de Pacientes para Consultório");

app.MapPost("/api/paciente/cadastrar/lista", ([FromBody] List<Paciente> x, [FromServices] AppDbContext ctx) =>
{
    ctx.TabelaPacientes.AddRange(x);
    ctx.SaveChanges();
    return Results.Ok();
});

app.MapGet("/api/paciente/listar", ([FromServices] AppDbContext ctx) =>
{
    List<Paciente> x = ctx.TabelaPacientes.ToList();

    if (x is null)
    {
        return Results.NoContent();
    }

    return Results.Ok(x);
});

app.MapGet("/api/paciente/buscar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Paciente? paciente = ctx.TabelaPacientes.Find(id);

    if (paciente is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(paciente);
});

app.MapPut("/api/paciente/editar/{id}", ([FromBody] Paciente x, [FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Paciente? paciente = ctx.TabelaPacientes.Find(id);

    if (paciente is null)
    {
        return Results.NotFound();
    }

    paciente.Endereco = x.Endereco;
    paciente.Idade = x.Idade;
    paciente.Nome = x.Nome;
    paciente.Sexo = x.Sexo;
    paciente.Telefone = x.Telefone;

    ctx.TabelaPacientes.Update(paciente);
    ctx.SaveChanges();
    return Results.Ok(paciente);   
});

app.MapDelete("/api/paciente/deletar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Paciente? paciente = ctx.TabelaPacientes.Find(id);

    if (paciente is null)
    {
        return Results.NotFound();
    }

    ctx.TabelaPacientes.Remove(paciente);
    ctx.SaveChanges();
    return Results.Ok();
});

app.MapPost("/api/consulta/cadastrar/lista", ([FromBody] List<Consulta> x, [FromServices] AppDbContext ctx) =>
{
    ctx.TabelaConsultas.AddRange(x);
    ctx.SaveChanges();
    return Results.Ok();
});

app.MapGet("/api/consulta/listar", ([FromServices] AppDbContext ctx) =>
{
    List<Consulta> x = ctx.TabelaConsultas.ToList();

    if (x is null)
    {
        return Results.NoContent();
    }

    return Results.Ok(x);
});

app.MapGet("/api/consulta/buscar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Consulta? consulta = ctx.TabelaConsultas.Find(id);

    if (consulta is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(consulta);
});

app.MapPut("/api/consulta/editar/{id}", ([FromBody] Consulta x, [FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Consulta? consulta = ctx.TabelaConsultas.Find(id);

    if (consulta is null)
    {
        return Results.NotFound();
    }

    consulta.DataConsulta = x.DataConsulta;
    consulta.Diagnostico = x.Diagnostico;
    consulta.Medico = x.Diagnostico;
    consulta.Tratamento = x.Tratamento;

    ctx.TabelaConsultas.Update(consulta);
    ctx.SaveChanges();
    return Results.Ok(consulta);   
});

app.MapDelete("/api/consulta/deletar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Consulta? consulta = ctx.TabelaConsultas.Find(id);

    if (consulta is null)
    {
        return Results.NotFound();
    }

    ctx.TabelaConsultas.Remove(consulta);
    ctx.SaveChanges();
    return Results.Ok();
});

app.Run();
