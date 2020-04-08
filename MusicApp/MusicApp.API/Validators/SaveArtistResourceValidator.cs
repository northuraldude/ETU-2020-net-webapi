using FluentValidation;
using MusicApp.API.Resources;

namespace MusicApp.API.Validators
{
    public class SaveArtistResourceValidator : AbstractValidator<SaveArtistResource>
    {
        public SaveArtistResourceValidator()
        {
            const int maxLength = 50;
            
            RuleFor(a => a.Name).NotEmpty().MaximumLength(maxLength);
        }
    }
}