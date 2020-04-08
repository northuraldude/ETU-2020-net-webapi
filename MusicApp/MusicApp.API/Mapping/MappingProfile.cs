using AutoMapper;
using MusicApp.API.Resources;
using MusicApp.Core.Models;

namespace MusicApp.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Music, MusicResource>().ReverseMap();
            CreateMap<Artist, ArtistResource>().ReverseMap();
            
            CreateMap<SaveMusicResource, Music>();
            CreateMap<SaveArtistResource, Artist>();
        }
    }
}