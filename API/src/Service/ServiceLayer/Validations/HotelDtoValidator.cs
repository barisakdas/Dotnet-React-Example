namespace ServiceLayer.Validations;

public class HotelDtoValidator : AbstractValidator<HotelDto>
{
    public HotelDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(x => x.Stars).InclusiveBetween(1, 5).WithMessage("{PropertyName} must be greater than 0.");
    }
}
