using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FilmReference.DataAccess
{
    [ModelMetadataType(typeof(PersonModelMetadata))]
    public partial class Person : IValidatableObject
    {
        private readonly FilmReferenceContext _context;

        public Person(FilmReferenceContext context)
        {
            _context = context;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Must be either a director or an actor
            if (IsActor == false && IsDirector == false)
            {
                results.Add(new ValidationResult(
                    "Must be either an actor or a director",
                    new List<string>
                    {
                        nameof(IsActor),
                        nameof(IsDirector)
                    }));
            }

            // Must have either a first or last name
            if (string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
            {
                results.Add(new ValidationResult(
                    "Must have either a first or last name.",
                    new List<string>
                    {
                        nameof(FirstName),
                        nameof(LastName)
                    }));
            }

            IEnumerable<Person> duplicates = _context.Person;
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                duplicates = duplicates
                .Where(
                    p =>
                        p.LastName != null &&
                        p.LastName.ToLower().Replace(" ", "") == LastName.ToLower().Replace(" ", ""));
            }
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                duplicates = duplicates
                    .Where(
                        p =>
                            p.FirstName != null &&
                            p.FirstName.ToLower().Replace(" ", "") == FirstName.ToLower().Replace(" ", ""));
            }
            if (PersonId > 0) // It's an update
            {
                duplicates = duplicates.Where(p => p.PersonId != PersonId);
            }
            if (duplicates.Any())
            {
                results.Add(new ValidationResult(
                    "A Person with this first and last name already exists in the database",
                    new List<string>
                    {
                        nameof(FirstName),
                        nameof(LastName)
                    }));
            }

            return results;
        }
    }
}
