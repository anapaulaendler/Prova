using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseInMemoryDatabase("BibliotecaDB"));
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});
var app = builder.Build();

SeedDatabase(app);

app.MapPost("/api/livro/cadastrar", ([FromBody] Livro x, [FromServices] BibliotecaContext ctx) =>
{
    ctx.Livros.Add(x);
    ctx.SaveChanges();
    return Results.Ok(x);
});

app.MapGet("/api/livro/listar", ([FromServices] BibliotecaContext ctx) =>
{
    List<Livro> x = ctx.Livros.ToList();

    foreach(Livro y in ctx.Livros.ToList())
    {
        if (y.Autor is null)
        {
            Autor? z = ctx.Autores.Find(y.AutorId);
            y.Autor = z;
        }
    }


    
    if (x is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(x);
});

app.MapGet("/api/livro/buscar/{id}", ([FromRoute] string id, [FromServices] BibliotecaContext ctx) =>
{
    Livro? x = ctx.Livros.Find(id);

    if (x is null)
    {
        return Results.NotFound("Id não encontrado.");
    }

    return Results.Ok(x);
});

app.MapPut("/api/livro/editar/{id}", ([FromBody] Livro xAlterado, [FromRoute] string id, [FromServices] BibliotecaContext ctx) =>
{
    Livro? x = ctx.Livros.Find(id);

    if (x is null)
    {
        return Results.NotFound("Id não encontrado.");
    }

    x.AnoPublicacao = xAlterado.AnoPublicacao;
    x.Autor = xAlterado.Autor;
    x.Titulo = xAlterado.Titulo;

    ctx.Livros.Update(x);
    ctx.SaveChanges();
    return Results.Ok(x);
});

app.MapDelete("/api/livro/deletar/{id}", ([FromRoute] string id, [FromServices] BibliotecaContext ctx) =>
{
    Livro? x = ctx.Livros.Find(id);

    if (x is null)
    {
        return Results.NotFound("Id não encontrado.");
    }

    ctx.Livros.Remove(x);
    ctx.SaveChanges();
    return Results.Ok();
});

app.MapPost("/api/autor/cadastrar", ([FromBody] Autor x, [FromServices] BibliotecaContext ctx) =>
{
    ctx.Autores.Add(x);
    ctx.SaveChanges();
    return Results.Ok(x);
});

app.MapGet("/api/autor/listar", ([FromServices] BibliotecaContext ctx) =>
{
    List<Autor> x = ctx.Autores.ToList();
    
    foreach (Autor y in ctx.Autores.ToList())
    {
        var livros = ctx.Livros.Where(x => x.AutorId == y.AutorId);
        y.Livros = livros.ToList();
    }

    if (x is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(x);
});

app.MapGet("/api/autor/buscar/{id}", ([FromRoute] string id, [FromServices] BibliotecaContext ctx) =>
{
    Autor? x = ctx.Autores.Find(id);

    if (x is null)
    {
        return Results.NotFound("Id não encontrado.");
    }

    return Results.Ok(x);
});

app.MapPut("/api/autor/editar/{id}", ([FromBody] Autor xAlterado, [FromRoute] string id, [FromServices] BibliotecaContext ctx) =>
{
    Autor? x = ctx.Autores.Find(id);

    if (x is null)
    {
        return Results.NotFound("Id não encontrado.");
    }

    x.DataNascimento = xAlterado.DataNascimento;
    x.Livros = xAlterado.Livros;
    x.Nome = xAlterado.Nome;

    ctx.Autores.Update(x);
    ctx.SaveChanges();
    return Results.Ok(x);
});

app.MapDelete("/api/autor/deletar/{id}", ([FromRoute] string id, [FromServices] BibliotecaContext ctx) =>
{
    Autor? x = ctx.Autores.Find(id);

    if (x is null)
    {
        return Results.NotFound("Id não encontrado.");
    }

    ctx.Autores.Remove(x);
    ctx.SaveChanges();
    return Results.Ok();
});

app.Run();

void SeedDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<BibliotecaContext>();

        // Limpa o banco antes de adicionar os dados (opcional)
        context.Livros.RemoveRange(context.Livros);
        context.Autores.RemoveRange(context.Autores);
        context.SaveChanges();

        // Criação de autores
        var autor1 = new Autor { Nome = "Gabriel Garcia Marquez", DataNascimento = new DateTime(1927, 3, 6) };
        var autor2 = new Autor { Nome = "Isabel Allende", DataNascimento = new DateTime(1942, 8, 2) };
        var autor3 = new Autor { Nome = "Jorge Luis Borges", DataNascimento = new DateTime(1899, 8, 24) };
        var autor4 = new Autor { Nome = "Mario Vargas Llosa", DataNascimento = new DateTime(1936, 3, 28) };
        var autor5 = new Autor { Nome = "Pablo Neruda", DataNascimento = new DateTime(1904, 7, 12) };
        var autor6 = new Autor { Nome = "Clarice Lispector", DataNascimento = new DateTime(1920, 12, 10) };
        var autor7 = new Autor { Nome = "Machado de Assis", DataNascimento = new DateTime(1839, 6, 21) };

        // Criação de livros com autores associados
        var livro1 = new Livro { Titulo = "Cem Anos de Solidão", AnoPublicacao = 1967, Autor = autor1, AutorId = "8154c11a-c0b3-4e45-9928-8040be2bc016" };
        var livro2 = new Livro { Titulo = "Amor nos Tempos do Cólera", AnoPublicacao = 1985, Autor = autor1, AutorId = "8154c11a-c0b3-4e45-9928-8040be2bc016" };
        var livro3 = new Livro { Titulo = "A Casa dos Espíritos", AnoPublicacao = 1982, Autor = autor2, AutorId = "edb7d750-ddf6-4a3e-9704-9788f5e2e75a" };
        var livro4 = new Livro { Titulo = "O Aleph", AnoPublicacao = 1949, Autor = autor3, AutorId = "0e403a04-4b09-413f-a6a8-ee6d4e44b231" };
        var livro5 = new Livro { Titulo = "A Cidade e os Cachorros", AnoPublicacao = 1963, Autor = autor4, AutorId = "702e1949-7ed0-4ef2-9ef0-7c43eaaa83a4" };
        var livro6 = new Livro { Titulo = "Confesso que Vivi", AnoPublicacao = 1974, Autor = autor5 };
        var livro7 = new Livro { Titulo = "A Paixão Segundo G.H.", AnoPublicacao = 1964, Autor = autor6 };
        var livro8 = new Livro { Titulo = "Dom Casmurro", AnoPublicacao = 1899, Autor = autor7 };
        var livro9 = new Livro { Titulo = "Memórias Póstumas de Brás Cubas", AnoPublicacao = 1881, Autor = autor7 };

        // Adiciona os autores e livros ao contexto
        context.Autores.AddRange(autor1, autor2, autor3, autor4, autor5, autor6, autor7);
        context.Livros.AddRange(livro1, livro2, livro3, livro4, livro5, livro6, livro7, livro8, livro9);

        // Salva as mudanças no banco em memória
        context.SaveChanges();
    }
}