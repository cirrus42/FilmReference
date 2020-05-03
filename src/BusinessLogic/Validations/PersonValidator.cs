using BusinessLogic.Models;
using System.Collections.Generic;

namespace BusinessLogic.Validations
{
    public class PersonValidator : IPersonValidator
    {
        public IEnumerable<string> ValidatePerson(Person person)
        {
            var results = new List<string>();

            if (!person.Actor && !person.Director)
                results.Add(PageValues.PersonTypeValidation);
            
            if (string.IsNullOrWhiteSpace(person.FirstName) && string.IsNullOrWhiteSpace(person.LastName))
              results.Add(PageValues.PersonNameValidation);

            return results;
        }
    }
}
