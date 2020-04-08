using FluentValidation;
using MusicApp.API.Resources;

namespace MusicApp.API.Validators
{
    public class SaveMusicResourceValidator : AbstractValidator<SaveMusicResource>
    {
        public SaveMusicResourceValidator()
        {
            const int maxLength = 50;
            const string errorMsg = "'Artist Id' must be greater than 0.";
            
            RuleFor(m => m.Name).NotEmpty().MaximumLength(maxLength);
            
            RuleFor(m => m.ArtistId).NotEmpty().WithMessage(errorMsg);
        }
    }
}