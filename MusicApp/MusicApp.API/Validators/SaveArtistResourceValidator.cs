using FluentValidation;
using MusicApp.API.Resources;

namespace MusicApp.API.Validators
{
    public class SaveArtistResourceValidator : AbstractValidator<SaveArtistResource>
    {
        public SaveArtistResourceValidator()
        {
            RuleFor(a => a.Name).NotEmpty()
                                                           .MaximumLength(50);
        }
    }
}