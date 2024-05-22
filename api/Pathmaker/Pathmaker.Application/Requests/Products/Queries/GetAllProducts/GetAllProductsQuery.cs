using MediatR;

namespace Pathmaker.Application.Requests.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<ProductDto[]> {
}