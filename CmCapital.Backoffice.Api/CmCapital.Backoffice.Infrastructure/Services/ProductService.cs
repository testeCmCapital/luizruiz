using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Domain.Entities;
using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;
using CmCapital.Backoffice.Infrastructure.Validators.FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace CmCapital.Backoffice.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<Product?>> CreateProductAsync(Product product) 
    {
        var validation = await ValideProductAsync(product);

        if (!validation.IsValid)
            return Result<Product?>.BadRequest(validation.Errors[0].ErrorMessage);

        var created = await _productRepository.AddAsync(product);

        return created
            ? Result<Product?>.Created(product)
            : Result<Product?>.BadRequest("Não foi possivel cadastrar o produto");
    }

    public async Task<Result<IEnumerable<Product?>>> GetProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();

        if (!products.Any())
            return Result<IEnumerable<Product?>>.Success();

        return Result<IEnumerable<Product?>>.Success(products);
    }

    public async Task<Result<Product?>> UpdateProductAsync(Product product) 
    {
        var validation = await ValideProductAsync(product);

        if (!validation.IsValid)
            return Result<Product?>.BadRequest(validation.Errors[0].ErrorMessage);

        var updated = await _productRepository.UpdateAsync(product);

        return Result<Product?>.Success(updated);
    }

    public async Task<Result<bool?>> DeleteProductAsync(Guid id)
    {
        var deleted = await _productRepository.DeleteAsync(id);

        return deleted
            ? Result<bool?>.Success()
            : Result<bool?>.Conflict("Não foi possivel deletar o produto");
    }

    public async Task<Result<Product?>> GetProductAsync(Guid id)
    {
        var product = await _productRepository.GetProductAsync(id);

        return product is null
            ? Result<Product?>.BadRequest("Não foi possivel encontrar o produto")
            : Result<Product?>.Success(product);
    }

    public async Task<Result<int?>> CountProductsAsync() 
    {
        var count = await _productRepository.CountAsync();

        return Result<int?>.Success(count);
    }

    public async Task<Result<IEnumerable<Product?>>> ListProductAsync(
        string category,
        float balance,
        float initialValue,
        DateTime expirationDate)
    {
        var products = await _productRepository
            .ListProductAsync(BuildPredicate(category, balance, initialValue, expirationDate));

        if (!products.Any())
            return Result<IEnumerable<Product?>>.Success(new List<Product>());

        return Result<IEnumerable<Product?>>.Success(products.Take(3));
    }

    private static async Task<ValidationResult> ValideProductAsync(Product product)
        => await (new ProductValidator()
            .ValidateAsync(product));

    private static Expression<Func<Product, bool>> BuildPredicate(
        string category, 
        float balance,
        float initialValue,
        DateTime expirationDate) 
    {
        var residual = (initialValue * 0.2);
        var spend = balance - residual;

        return product => product.Category == category
                        && product.UnitValue < spend
                        && product.ExpirationDate.AddMonths(-4) < expirationDate;
    }
}