using FilmReference.DataAccess;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace FilmReference.FrontEnd.Extensions
{
    public static class FilmPagesExtensions
    {
        public static IEnumerable<FilmPerson> RemoveItems(
            this ICollection<FilmPerson> filmPersonCollection,
            IEnumerable<int> updateList)
        { 
            var filmPersonList = filmPersonCollection.ToList();
            var updated = updateList.ToList();
            filmPersonList.RemoveAll(filmPerson => updated.Contains(filmPerson.PersonId));

            return filmPersonList;
        }

        public static IEnumerable<int> RemoveItems(
            this IEnumerable<int> updateList,
            ICollection<FilmPerson> filmPersonCollection)
        {
            var updated = updateList.ToList();
            var filmPersonList = filmPersonCollection.ToList();

            foreach (var filmPerson in filmPersonList)
                updated.Remove(filmPerson.PersonId);

            return updated;
        }

        public static IEnumerable<int> StingValuesToList(this StringValues stringValues)
        {
            var idList = new List<int>();
            foreach (var id in stringValues)
            {
                var parsed = int.TryParse(id, out var number);
                if(parsed)
                    idList.Add(number);
            }

            return idList;
        }
    }
}
