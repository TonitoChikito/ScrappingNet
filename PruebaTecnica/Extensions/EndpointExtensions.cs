using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Models;
using System.Net;
using HtmlAgilityPack;

public static class EndpointExtensions
{
    public static void MapNewsEndpoints(this WebApplication app)
    {
        app.MapGet("/noticias", async (AppDbContext db) =>
        {
            var noticias = await db.Noticias.ToListAsync();
            return Results.Ok(noticias);
        });

        app.MapGet("/noticia/{id}", async (int id, AppDbContext db) =>
        {
            var noticia = await db.Noticias.FindAsync(id);
            return noticia is not null ? Results.Ok(noticia) : Results.NotFound("Noticia no encontrada.");
        });

        app.MapPost("/noticia", async (int id, Noticia noticia, AppDbContext db) =>
        {
            var existing = await db.Noticias.FindAsync(id);
            if (existing == null)
                return Results.NotFound("Noticia no encontrada.");

            existing.Titulo = noticia.Titulo;
            existing.Descripcion = noticia.Descripcion;
            await db.SaveChangesAsync();
            return Results.Ok(existing);
        });

        app.MapDelete("/noticia/{id}", async (int id, AppDbContext db) =>
        {
            var noticia = await db.Noticias.FindAsync(id);
            if (noticia == null)
                return Results.NotFound("Noticia no encontrada.");

            db.Noticias.Remove(noticia);
            await db.SaveChangesAsync();
            return Results.Ok("Noticia eliminada.");
        });

        app.MapPut("/noticia/{id}", async (int id, Noticia noticia, AppDbContext db) =>
        {
            var existingNoticia = await db.Noticias.FindAsync(id);
            if (existingNoticia == null)
            {
                return Results.NotFound("Noticia no encontrada.");
            }

            existingNoticia.Titulo = noticia.Titulo;
            existingNoticia.Descripcion = noticia.Descripcion;

            await db.SaveChangesAsync();
            return Results.Ok(existingNoticia);
        });
        
        app.MapPost("/noticia", async (Noticia noticia, AppDbContext db) =>
        {
            db.Noticias.Add(noticia);
            await db.SaveChangesAsync();
            return Results.Created($"/noticia/{noticia.Id}", noticia);
        });

        app.MapGet("/scrape", async (AppDbContext db) =>
        {
            var url = "https://www.bbc.com/news";
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(url);

            var titulos = doc.DocumentNode.SelectNodes("//h2[contains(@data-testid,'card-headline')]");
            var descripciones = doc.DocumentNode.SelectNodes("//p[contains(@data-testid,'card-description')]");

            if (titulos == null || descripciones == null)
                return Results.NotFound("No se encontraron noticias.");

            int nuevas = 0;

            for (int i = 0; i < Math.Min(titulos.Count, descripciones.Count); i++)
            {
                string titulo = WebUtility.HtmlDecode(titulos[i].InnerText.Trim());
                string descripcion = WebUtility.HtmlDecode(descripciones[i].InnerText.Trim());

                bool existe = await db.Noticias.AnyAsync(n => n.Titulo == titulo && n.Descripcion == descripcion);
                if (!existe)
                {
                    db.Noticias.Add(new Noticia { Titulo = titulo, Descripcion = descripcion });
                    nuevas++;
                }
            }

            await db.SaveChangesAsync();
            return Results.Ok($"Scraping completado. {nuevas} noticias nuevas agregadas.");
        });
    }
}
