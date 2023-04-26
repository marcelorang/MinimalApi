using ApiCatalogo2.Context;
using ApiCatalogo2.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo2.ApiEndpoints
{
    public static class ProdutosEndpoints
    {
        public static void MapProdutosEndpoints (this WebApplication app)
        {
            ///----------------------------------------------endpoints Produtos------------------------------------///

            app.MapPost("/produtos", async (Produto produto, AppDbContext db) =>
            {
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();
                return Results.Created($"/produtos/{produto.Id}", produto);
            });

            app.MapGet("/produtos", async (AppDbContext db) =>
            await db.Produtos.ToListAsync()).WithTags("Produtos").RequireAuthorization();

            app.MapGet("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Produtos.FindAsync(id)
                is Produto produto ? Results.Ok(produto) : Results.NotFound("ID não encontrada");

            });

            app.MapPut("/produtos/{id:int}", async (int id, Produto produto, AppDbContext db) =>
            {
                if (produto.Id != id)
                    return Results.BadRequest();


                //produtosDB representa os dados que eu vou alterar
                var produtoDB = await db.Produtos.FindAsync(id);

                if (produtoDB is null)
                    return Results.NotFound();

                //atrubuição a Produto a variável
                produtoDB.Nome = produto.Nome;
                produtoDB.Descricao = produto.Descricao;
                produtoDB.Preco = produto.Preco;
                produtoDB.Imagem = produto.Imagem;
                produtoDB.DataCompra = produto.DataCompra;
                produtoDB.CategoriaId = produto.CategoriaId;
                produtoDB.Descricao = produto.Descricao;


                await db.SaveChangesAsync();
                return Results.Ok(produtoDB);
            });

            app.MapDelete("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                var produto = await db.Produtos.FindAsync(id);

                if (produto is null)
                    return Results.NotFound();

                db.Produtos.Remove(produto);
                await db.SaveChangesAsync();
                return Results.NoContent();

            });

        }
    }
}
