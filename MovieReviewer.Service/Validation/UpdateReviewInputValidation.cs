using FluentValidation;
using MovieReviewer.Core.DTO.Inputs;

namespace MovieReviewer.Service.Validation;

public class UpdateReviewInputValidation : AbstractValidator<UpdateReviewInputModel>
{
    public UpdateReviewInputValidation()
    {
        RuleFor(model => model.IsDisabled).NotEmpty().WithMessage("Please specify it is disabled.");
        RuleFor(model => model.ReviewContent).NotEmpty().WithMessage("Reviews can't be empty.");
        RuleFor(model => model.ReviewScore).InclusiveBetween(1, 10).WithMessage("Review score must be between 1.0 and 10.");
    }
}