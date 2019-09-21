using MvcMovie.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Core.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> ListAsync(string movieGenre, string searchString);
        Task<Movie> FindAsync(int id);
        Task Add(Movie movie);
        Task Remove(Movie movie);
        Task Update(Movie movie);
        bool Exists(int id);
        IQueryable<string> GenreList();
    }
}
