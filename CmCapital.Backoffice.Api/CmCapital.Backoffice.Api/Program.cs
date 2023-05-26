using CmCapital.Backoffice.Application.Commands.Request;
using CmCapital.Backoffice.Application.Queries.Request;
using CmCapital.Backoffice.Infrastructure.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/product", async (
    [FromBody] CreateProductRequest request,
    ISender sender,
    CancellationToken cancellationToken) 
=>
{
    var create = await sender.Send(request, cancellationToken);

    return Results.Ok();
});

app.MapGet("/product", async (
    ISender sender,
    CancellationToken cancellationToken) 
=>
{
    var listProdct = await sender.Send(new ListProductRequest(), cancellationToken);

    return Results.Ok(listProdct.Value);
});

app.MapPost("/client", async (
    [FromBody] CreateClientRequest request,
    ISender sender,
    CancellationToken cancellationToken)
=>
{
    var create = await sender.Send(request, cancellationToken);

    return Results.Ok();
});

app.MapGet("/client", async (
    ISender sender,
    CancellationToken cancellationToken)
=>
{
    var listProdct = await sender.Send(new ListClientRequest(), cancellationToken);

    return Results.Ok(listProdct.Value);
});

app.MapPost("/cliente-compra-produto", async (
    [FromBody] BuyProductRequest request,
    ISender sender,
    CancellationToken cancellationToken)
=>
{
    var create = await sender.Send(request, cancellationToken);

    return Results.Ok();
});

app.MapPost("/cliente-desiste-compra-produto ", async (
    [FromBody] RollbackPurchaseRequest request,
    ISender sender,
    CancellationToken cancellationToken)
=>
{
    var create = await sender.Send(request, cancellationToken);

    return Results.Ok();
});

app.MapGet("/produtos-comprados-por-cliente/{id}", async (
    Guid id,
    ISender sender,
    CancellationToken cancellationToken)
=>
{
    var listProdct = await sender.Send(new ListProductByClientRequest(id), cancellationToken);

    return Results.Ok(listProdct.Value);
});

app.MapGet("/produtos-menos-vendido", async (
    ISender sender,
    CancellationToken cancellationToken)
=>
{
    var productStatistics = await sender.Send(new GetLessSoldProductRequest(), cancellationToken);

    return Results.Ok(productStatistics.Value);
});

app.MapGet("/produtos-mais-vendido", async (
    ISender sender,
    CancellationToken cancellationToken)
=>
{
    var productStatistics = await sender.Send(new GetMostSoldProductRequest(), cancellationToken);

    return Results.Ok(productStatistics.Value);
});

app.MapGet("/Juros-dos-anos", async (
    ISender sender,
    CancellationToken cancellationToken)
=>
{
    var productStatistics = await sender.Send(new GetHonorariumOfYearRequest(), cancellationToken);

    return Results.Ok(productStatistics.Value);
});


app.Run();