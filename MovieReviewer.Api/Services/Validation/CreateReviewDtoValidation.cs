using FluentValidation;
using MovieReviewer.Shared.Dto.Input;

namespace MovieReviewer.Service.Validation;

public class CreateReviewDtoValidation : AbstractValidator<CreateReviewDto>
{
    public CreateReviewDtoValidation()
    {
        RuleFor(model => model.Content).NotEmpty().WithMessage("Review content cannot be empty");
        RuleFor(model => model.Score).InclusiveBetween(1, 10).WithMessage("Review score must be between 1 and 10");
    }
}