using AutoMapper;
using MusicApp.API.Resources;
using MusicApp.Core.Models;

namespace MusicApp.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Music, MusicResource>();
            CreateMap<Artist, ArtistResource>();
            
            CreateMap<MusicResource, Music>();
            CreateMap<SaveMusicResource, Music>();
            CreateMap<ArtistResource, Artist>();
            CreateMap<SaveArtistResource, Artist>();
        }
    }
}