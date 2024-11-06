using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();


app.MapGet("/", () => "API de Produtos!");

app.MapPost("/api/fornecedor/cadastrar", ([FromBody] Fornecedor f, [FromServices] AppDbContext ctx) =>
{
    ctx.TabelaFornecedores.Add(f);
    ctx.SaveChanges();
    return Results.Ok(f);
});

app.MapPost("/api/fornecedor/cadastrar/lista", ([FromBody] List<Fornecedor> f, [FromServices] AppDbContext ctx) =>
{
    ctx.TabelaFornecedores.AddRange(f);
    ctx.SaveChanges();
    return Results.Ok(f);
});

app.MapGet("/api/fornecedor/listar", ([FromServices] AppDbContext ctx) =>
{
    if (ctx.TabelaFornecedores.Any())
    {
        List<Fornecedor> f = ctx.TabelaFornecedores.ToList();
        return Results.Ok(f);
    }

    return Results.NotFound();
});

app.MapGet("/api/fornecedor/buscar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Fornecedor? f = ctx.TabelaFornecedores.Find(id);

    if (f is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(f);
});

app.MapPut("/api/fornecedor/alterar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx, [FromBody] Fornecedor fAlterado) =>
{
    Fornecedor? f = ctx.TabelaFornecedores.Find(id);

    if (f is null)
    {
        return Results.NotFound();
    }

    f.Contato = fAlterado.Contato;
    f.Endereco = fAlterado.Endereco;
    f.Nome = fAlterado.Nome;

    ctx.TabelaFornecedores.Update(f);
    ctx.SaveChanges();
    return Results.Ok(f);
});

app.MapDelete("/api/fornecedor/deletar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Fornecedor? f = ctx.TabelaFornecedores.Find(id);

    if (f is null)
    {
        return Results.NotFound();
    }

    ctx.TabelaFornecedores.Remove(f);
    ctx.SaveChanges();
    return Results.Ok();
});

app.MapPost("/api/pedido/cadastrar", ([FromBody] Pedido p, [FromServices] AppDbContext ctx) =>
{
    ctx.TabelaPedidos.Add(p);
    ctx.SaveChanges();
    return Results.Ok(p);
});

app.MapPost("/api/pedido/cadastrar/lista", ([FromBody] List<Pedido> p, [FromServices] AppDbContext ctx) =>
{
    ctx.TabelaPedidos.AddRange(p);
    ctx.SaveChanges();
    return Results.Ok(p);
});

app.MapGet("/api/pedido/listar", ([FromServices] AppDbContext ctx) =>
{
    if (ctx.TabelaPedidos.Any())
    {
        List<Pedido> p = ctx.TabelaPedidos.ToList();
        return Results.Ok(p);
    }

    return Results.NotFound();
});

app.MapGet("/api/pedido/buscar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Pedido? p = ctx.TabelaPedidos.Find(id);

    if (p is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(p);
});

app.MapPut("/api/pedido/alterar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx, [FromBody] Pedido pAlterado) =>
{
    Pedido? p = ctx.TabelaPedidos.Find(id);

    if (p is null)
    {
        return Results.NotFound();
    }

    p.DataPedido = pAlterado.DataPedido;
    p.Produto = pAlterado.Produto;
    p.Quantidade = pAlterado.Quantidade;
    p.Status = pAlterado.Status;

    ctx.TabelaPedidos.Update(p);
    ctx.SaveChanges();
    return Results.Ok(p);
});

app.MapDelete("/api/pedido/deletar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Pedido? p = ctx.TabelaPedidos.Find(id);

    if (p is null)
    {
        return Results.NotFound();
    }

    ctx.TabelaPedidos.Remove(p);
    ctx.SaveChanges();
    return Results.Ok();
});

app.MapPost("/api/produto/cadastrar", ([FromBody] Produto p, [FromServices] AppDbContext ctx) =>
{
    ctx.TabelaProdutos.Add(p);
    ctx.SaveChanges();
    return Results.Ok(p);
});

app.MapPost("/api/produto/cadastrar/lista", ([FromBody] List<Produto> p, [FromServices] AppDbContext ctx) =>
{
    ctx.TabelaProdutos.AddRange(p);
    ctx.SaveChanges();
    return Results.Ok(p);
});

app.MapGet("/api/produto/listar", ([FromServices] AppDbContext ctx) =>
{
    if (ctx.TabelaProdutos.Any())
    {
        List<Produto> p = ctx.TabelaProdutos.ToList();
        return Results.Ok(p);
    }

    return Results.NotFound();
});

app.MapGet("/api/produto/buscar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Produto? p = ctx.TabelaProdutos.Find(id);

    if (p is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(p);
});

app.MapPut("/api/produto/alterar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx, [FromBody] Produto pAlterado) =>
{
    Produto? p = ctx.TabelaProdutos.Find(id);

    if (p is null)
    {
        return Results.NotFound();
    }

    p.Descricao = pAlterado.Descricao;
    p.Fornecedor = pAlterado.Fornecedor;
    p.Nome = pAlterado.Nome;
    p.Preco = pAlterado.Preco;
    p.Quantidade = p.Quantidade;

    ctx.TabelaProdutos.Update(p);
    ctx.SaveChanges();
    return Results.Ok(p);
});

app.MapDelete("/api/produto/deletar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Produto? p = ctx.TabelaProdutos.Find(id);

    if (p is null)
    {
        return Results.NotFound();
    }

    ctx.TabelaProdutos.Remove(p);
    ctx.SaveChanges();
    return Results.NoContent();
});

app.MapGet("/api/pedido/ativo", ([FromServices] AppDbContext ctx) =>
{
    List<Pedido> p = ctx.TabelaPedidos.Where(x => x.Status).ToList();

    if (p is null)
    {
        return Results.NotFound();
    }
    
    return Results.Ok(p);
});

app.Run();
