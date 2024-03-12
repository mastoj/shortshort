using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/shorten", ([FromQuery] string url) =>
{
  var shortService = new ShortService();
  var id = shortService.Shorten(url);
  return new { id, url };
});

app.MapGet("/{id}", (string id) =>
{
  var shortService = new ShortService();
  var url = shortService.Expand(id);
  if (url == null)
  {
    return Results.NotFound();
  }
  return Results.Ok(url);
});

app.Run();
