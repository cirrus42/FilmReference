using BusinessLogic.Models;
using System.Collections.Generic;

namespace BusinessLogic.Validations
{
    public interface IPersonValidator
    {
        public IEnumerable<string> ValidatePerson(Person person);
    }
}