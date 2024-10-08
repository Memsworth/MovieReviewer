using FluentValidation;
using MovieReviewer.Shared.Core.DTO.Inputs;

namespace MovieReviewer.Shared.Core.Validation;

public class CreateReviewInputValidation : AbstractValidator<CreateReviewInputModel>
{
    public CreateReviewInputValidation()
    {
        RuleFor(model => model.ReviewContent).NotEmpty().WithMessage("Review content cannot be empty");
        RuleFor(model => model.ReviewScore).InclusiveBetween(1, 10).WithMessage("Review score must be between 1 and 10");
    }
}