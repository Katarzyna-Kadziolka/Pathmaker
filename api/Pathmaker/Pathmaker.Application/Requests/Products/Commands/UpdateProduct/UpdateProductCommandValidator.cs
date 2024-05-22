using FluentValidation;

namespace Pathmaker.Application.Requests.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand> {
    public UpdateProductCommandValidator() {
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(10).NotEmpty();
    }
}