using FluentValidation;
using MovieReviewer.Core.DTO.Inputs;

namespace MovieReviewer.Service.Validation;

public class CreateReviewInputValidation : AbstractValidator<CreateReviewInputModel>
{
    public CreateReviewInputValidation()
    {
        RuleFor(model => model.ReviewContent).NotEmpty().WithMessage("Review content cannot be empty");
        RuleFor(model => model.ReviewScore).InclusiveBetween(1, 10).WithMessage("Review score must be between 1 and 10");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateReviewInputModel>
            .CreateWithOptions((CreateReviewInputModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}