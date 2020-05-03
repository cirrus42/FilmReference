using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Validations;

namespace FilmReference.FrontEnd.Pages.PersonPages
{
    public class CreateModel : PageModel
    {
        private readonly IImageHelper _imageHelper;
        private readonly IPersonPagesManager _personPagesManager;
        //private readonly IPersonValidator _personValidator;
        public Person Person { get; set; }

        public CreateModel(IImageHelper imageHelper, IPersonPagesManager personPagesManager)
        {
            _imageHelper = imageHelper;
            _personPagesManager = personPagesManager;
        }

        public IActionResult OnGet() =>
            Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var newPerson = new Person();
            var updated = await TryUpdateModelAsync(
                newPerson,
                nameof(Person),
                p => p.Id,
                p => p.FirstName,
                p => p.LastName,
                p => p.Description,
                p => p.Actor,
                p => p.Director,
                p => p.Picture);

            if (!updated) return Page();

            var files = Request.Form.Files;
            if (files.Any())
            {
                var file = files.ElementAt(0);
                if (file.Length > 0)
                {
                    if (!_imageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.PersonPicture, errorMessage);
                        return Page();
                    }
                    
                    _imageHelper.AddImageToEntity(newPerson, file);
                }
            }

            var validationList = (await _personPagesManager.SavePerson(newPerson)).ToList();

            if (validationList.Count == 0)
            {
                var nextPage = !newPerson.Actor && newPerson.Director
                    ? PageValues.DirectorIndexPage
                    : PageValues.PersonIndexPage;
                return RedirectToPage(nextPage);
            }

            ModelState.AddModelStateValidation(validationList);

            return Page();
        }
    }
}