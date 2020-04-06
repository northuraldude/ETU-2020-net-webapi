using FluentValidation;
using MusicApp.API.Resources;

namespace MusicApp.API.Validators
{
    public class SaveMusicResourceValidator : AbstractValidator<SaveMusicResource>
    {
        public SaveMusicResourceValidator()
        {
            RuleFor(m => m.Name).NotEmpty()
                                                            .MaximumLength(50);
            
            RuleFor(m => m.ArtistId).NotEmpty()
                                                                .WithMessage("'Artist Id' must be greater than 0.");
        }
    }
}