//using FilmReference.DataAccess.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;

//namespace FilmReference.DataAccess
//{
//    [ModelMetadataType(typeof(FilmModelMetadata))]
//    public partial class Film : IValidatableObject
//    {
//        private readonly IGenericRepository<Film> _filmRepository;
//        public Film(FilmReferenceContext context, IGenericRepository<Film> filmRepository) =>
//            _filmRepository = filmRepository;
        
//        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
//        {
//            var results = new List<ValidationResult>();

//            var duplicates =
//                 _filmRepository.GetWhere(f => f.Name.ToLower().Replace(" ", "") == Name.ToLower().Replace(" ", "")).Result;

//            if (FilmId > 0) // It's an update
//            {
//                duplicates = duplicates
//                    .Where(
//                        f =>
//                            f.FilmId != FilmId);
//            }
//            if (duplicates.Any())
//            {
//                results.Add(new ValidationResult(
//                    "A Film with this name already exists in the database",
//                    new List<string>
//                    {
//                        "Film.Name"
//                    }));
//            }

//            return results;
//        }
//    }
//}
