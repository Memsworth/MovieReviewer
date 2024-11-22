
/*namespace MovieReviewer.Service.Validation;

public class CreateMovieInputValidation : AbstractValidator<CreateMovieInputModel>
{
    public CreateMovieInputValidation()
    {
        Include(new MovieInputValidation());
        RuleFor(model => model.ImdbId).NotEmpty().WithMessage("ImdbId is required");
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateMovieInputModel>
            .CreateWithOptions((CreateMovieInputModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}*/