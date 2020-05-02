using AutoMapper;
using BusinessLogic.Extensions;
using BusinessLogic.Models;
using FilmReference.DataAccess.Entities;

namespace FilmReference.FrontEnd.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FilmEntity, Film>()
                .ForMember(opt => opt.Id, opt => opt.MapFrom(o => o.FilmId))
                .ForMember(opt => opt.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(opt => opt.Description, opt => opt.MapFrom(o => o.Description))
                .ForMember(opt => opt.Picture, opt => opt.MapFrom(o => o.Picture))
                .ForMember(opt => opt.GenreId, opt => opt.MapFrom(o => o.GenreId))
                .ForMember(opt => opt.DirectorId, opt => opt.MapFrom(o => o.DirectorId))
                .ForMember(opt => opt.StudioId, opt => opt.MapFrom(o => o.StudioId))
                .ForMember(opt => opt.Genre, opt => opt.MapFrom(o => o.Genre))
                .ForMember(opt => opt.Director, opt => opt.MapFrom(o => o.Director))
                .ForMember(opt => opt.Studio, opt => opt.MapFrom(o => o.Studio))
                .ForMember(opt => opt.FilmPerson, opt => opt.MapFrom(o => o.FilmPerson));

            CreateMap<Film, FilmEntity>()
                .ForMember(opt => opt.FilmId, opt => opt.MapFrom(o => o.Id))
                .ForMember(opt => opt.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(opt => opt.Description, opt => opt.MapFrom(o => o.Description))
                .ForMember(opt => opt.Picture, opt => opt.MapFrom(o => o.Picture))
                .ForMember(opt => opt.GenreId, opt => opt.MapFrom(o => o.GenreId))
                .ForMember(opt => opt.DirectorId, opt => opt.MapFrom(o => o.DirectorId))
                .ForMember(opt => opt.StudioId, opt => opt.MapFrom(o => o.StudioId))
                .ForMember(opt => opt.Genre, opt => opt.MapFrom(o => o.Genre))
                .ForMember(opt => opt.Director, opt => opt.MapFrom(o => o.Director))
                .ForMember(opt => opt.Studio, opt => opt.MapFrom(o => o.Studio))
                .ForMember(opt => opt.FilmPerson, opt => opt.MapFrom(o => o.FilmPerson));

            CreateMap<FilmPersonEntity, FilmPerson>()
                .ForMember(opt => opt.FilmPersonId, opt => opt.MapFrom(o => o.FilmPersonId))
                .ForMember(opt => opt.FilmId, opt => opt.MapFrom(o => o.FilmId))
                .ForMember(opt => opt.PersonId, opt => opt.MapFrom(o => o.PersonId))
                .ForMember(opt => opt.Film, opt => opt.MapFrom(o => o.Film))
                .ForMember(opt => opt.Person, opt => opt.MapFrom(o => o.Person));

            CreateMap<FilmPerson, FilmPersonEntity>()
                .ForMember(opt => opt.FilmPersonId, opt => opt.MapFrom(o => o.FilmPersonId))
                .ForMember(opt => opt.FilmId, opt => opt.MapFrom(o => o.FilmId))
                .ForMember(opt => opt.PersonId, opt => opt.MapFrom(o => o.PersonId))
                .ForMember(opt => opt.Film, opt => opt.MapFrom(o => o.Film))
                .ForMember(opt => opt.Person, opt => opt.MapFrom(o => o.Person));

            CreateMap<GenreEntity, Genre>()
                .ForMember(opt => opt.Id, opt => opt.MapFrom(o => o.GenreId))
                .ForMember(opt => opt.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(opt => opt.Description, opt => opt.MapFrom(o => o.Description))
                .ForMember(opt => opt.Film, opt => opt.MapFrom(o => o.Film));

            CreateMap<Genre, GenreEntity>()
                .ForMember(opt => opt.GenreId, opt => opt.MapFrom(o => o.Id))
                .ForMember(opt => opt.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(opt => opt.Description, opt => opt.MapFrom(o => o.Description))
                .ForMember(opt => opt.Film, opt => opt.MapFrom(o => o.Film));

            CreateMap<PersonEntity, Person>()
                .ForMember(opt => opt.Id, opt => opt.MapFrom(o => o.PersonId))
                .ForMember(opt => opt.FirstName, opt => opt.MapFrom(o => o.FirstName))
                .ForMember(opt => opt.LastName, opt => opt.MapFrom(o => o.LastName))
                .ForMember(opt => opt.FullName, opt => opt.MapFrom(o => o.FullName))
                .ForMember(opt => opt.Description, opt => opt.MapFrom(o => o.Description))
                .ForMember(opt => opt.IsActor, opt => opt.MapFrom(o => o.IsActor))
                .ForMember(opt => opt.IsDirector, opt => opt.MapFrom(o => o.IsDirector))
                .ForMember(opt => opt.Picture, opt => opt.MapFrom(o => o.Picture))
                .ForMember(opt => opt.Film, opt => opt.MapFrom(o => o.Film))
                .ForMember(opt => opt.FilmPerson, opt => opt.MapFrom(o => o.FilmPerson));

            CreateMap<Person, PersonEntity>()
                .ForMember(opt => opt.PersonId, opt => opt.MapFrom(o => o.Id))
                .ForMember(opt => opt.FirstName, opt => opt.MapFrom(o => o.FirstName))
                .ForMember(opt => opt.LastName, opt => opt.MapFrom(o => o.LastName))
                .ForMember(opt => opt.FullName, opt => opt.MapFrom(o => o.FirstName.BuildFullName(o.LastName)))
                .ForMember(opt => opt.Description, opt => opt.MapFrom(o => o.Description))
                .ForMember(opt => opt.IsActor, opt => opt.MapFrom(o => o.IsActor))
                .ForMember(opt => opt.IsDirector, opt => opt.MapFrom(o => o.IsDirector))
                .ForMember(opt => opt.Picture, opt => opt.MapFrom(o => o.Picture))
                .ForMember(opt => opt.Film, opt => opt.MapFrom(o => o.Film))
                .ForMember(opt => opt.FilmPerson, opt => opt.MapFrom(o => o.FilmPerson));

            CreateMap<StudioEntity, Studio>()
                .ForMember(opt => opt.Id, opt => opt.MapFrom(o => o.StudioId))
                .ForMember(opt => opt.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(opt => opt.Description, opt => opt.MapFrom(o => o.Description))
                .ForMember(opt => opt.Picture, opt => opt.MapFrom(o => o.Picture))
                .ForMember(opt => opt.Film, opt => opt.MapFrom(o => o.Film));

            CreateMap<Studio, StudioEntity>()
                .ForMember(opt => opt.StudioId, opt => opt.MapFrom(o => o.Id))
                .ForMember(opt => opt.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(opt => opt.Description, opt => opt.MapFrom(o => o.Description))
                .ForMember(opt => opt.Picture, opt => opt.MapFrom(o => o.Picture))
                .ForMember(opt => opt.Film, opt => opt.MapFrom(o => o.Film));
        }
    }
}