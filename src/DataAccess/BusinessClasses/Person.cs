using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmReference.DataAccess
{
    [ModelMetadataType(typeof(PersonModelMetadata))]
    public partial class Person : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Must be either a director or an actor
            if (IsActor == false && IsDirector == false)
                results.Add(new ValidationResult(
                    "Must be either an actor or a director",
                    new List<string>
                    {
                        nameof(IsActor),
                        nameof(IsDirector)
                    }));
            
            // Must have either a first or last name
            if (string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
                results.Add(new ValidationResult(
                    "Must have either a first or last name.",
                    new List<string>
                    {
                        nameof(FirstName),
                        nameof(LastName)
                    }));
            
            return results;
        }
    }
}