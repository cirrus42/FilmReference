﻿using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonEntity = FilmReference.DataAccess.Entities.PersonEntity;

namespace FilmReference.FrontEnd.Pages.PersonPages
{
    public class IndexModel : PageModel
    {
        public IImageHelper ImageHelper;
        private readonly IPersonHandler _personHandler;

        public IndexModel(IImageHelper imageHelper, IPersonHandler personHandler)
        {
            ImageHelper = imageHelper;
            _personHandler = personHandler;
        }

        public List<PersonEntity> ActorList { get;set; }

        public List<string> AtoZ { get; set; }

        public async Task OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                id = "A";

            ActorList = (await _personHandler.GetActors(id)).ToList();

            AtoZ = new List<string>();

            for (var i = 65; i <= 90; i ++)
                AtoZ.Add(((char)i).ToString());
        }
    }
}