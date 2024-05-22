using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pathmaker.Application.Behaviour.Exceptions;
using Pathmaker.Persistence;
using Pathmaker.Persistence.Entities.Products;

namespace Pathmaker.Application.Requests.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto> {
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(ApplicationDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
        var product = await _context.Products
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (product is null) throw new NotFoundException(typeof(ProductEntity), request.Id.ToString());

        _mapper.Map(request, product);
        await _context.SaveChangesAsync(cancellationToken);
        var productDto = _mapper.Map<ProductDto>(product);
        return productDto;
    }
}