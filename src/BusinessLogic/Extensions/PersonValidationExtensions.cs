using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace BusinessLogic.Extensions
{
    public static class PersonValidationExtensions
    {
        public static ModelStateDictionary AddModelStateValidation(this ModelStateDictionary modelStateDictionary,
            List<string> validationList)
        {
            if (validationList.Contains(PageValues.PersonNameValidation))
            {
                modelStateDictionary.AddModelError(PageValues.PersonFirstName, PageValues.PersonNameMissing);
                modelStateDictionary.AddModelError(PageValues.PersonLastName, PageValues.PersonNameMissing);
            }

            if (validationList.Contains(PageValues.PersonDuplicateValidation))
            {
                modelStateDictionary.AddModelError(PageValues.PersonFirstName, PageValues.DuplicatePerson);
                modelStateDictionary.AddModelError(PageValues.PersonLastName, PageValues.DuplicatePerson);
            }

            if (!validationList.Contains(PageValues.PersonTypeValidation)) return modelStateDictionary;

            modelStateDictionary.AddModelError(PageValues.PersonIsActor, PageValues.PersonTypeMissing);

            return modelStateDictionary;
        }
    }
}