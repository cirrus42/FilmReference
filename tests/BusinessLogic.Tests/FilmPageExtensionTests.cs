using FilmReference.FrontEnd.Extensions;
using FilmReference.FrontEnd.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace BusinessLogic.Tests
{
    public class FilmPageExtensionTests
    {
        [Fact]
        public void RemoveItemsPersonCollectionReturnsListOfFilmPersonToRemove()
        {
            var filmPerson1 = new FilmPerson { PersonId = 1 };
            var filmPerson2 = new FilmPerson { PersonId = 2 };
            var filmPerson3 = new FilmPerson { PersonId = 3 };
            var filmPerson4 = new FilmPerson { PersonId = 4 };

            var filmPersonCollection = new Collection<FilmPerson> { filmPerson1, filmPerson2, filmPerson3, filmPerson4 };
            var updateList = new List<int>{ filmPerson2.PersonId, filmPerson4.PersonId};

            var itemsToRemove = filmPersonCollection.RemoveItems(updateList);

            var filmPersonsList= itemsToRemove.ToList();
            filmPersonsList.Count().Should().Be(2);

            filmPersonsList.Should().Contain(filmPerson1);
            filmPersonsList.Should().Contain(filmPerson3);
        }

        [Fact]
        public void RemoveItemsIdListReturnsListOfIdToAdd()
        {
            var filmPerson1 = new FilmPerson { PersonId = 1 };
            var filmPerson2 = new FilmPerson { PersonId = 2 };
            var filmPerson3 = new FilmPerson { PersonId = 3 };
            var filmPerson4 = new FilmPerson { PersonId = 4 };

            var filmPersonCollection = new Collection<FilmPerson> { filmPerson1, filmPerson3 };
            var updateList = new List<int> { filmPerson2.PersonId, filmPerson3.PersonId, filmPerson4.PersonId };

            var itemsToAdd = updateList.RemoveItems(filmPersonCollection);

            var updatedList = itemsToAdd.ToList();

            updatedList.Count().Should().Be(2);

            updatedList.Should().Contain(filmPerson2.PersonId);
            updatedList.Should().Contain(filmPerson4.PersonId);
        }
    }
}
