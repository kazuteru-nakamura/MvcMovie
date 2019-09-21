using Microsoft.EntityFrameworkCore;
using MvcMovie.Core.Interfaces;
using MvcMovie.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Infrastructure
{
    public class EfMovieRepository : IMovieRepository
    {

        private readonly MvcMovieContext _context;

        public EfMovieRepository(MvcMovieContext context)
        {
            _context = context;
        }

        public Task<List<Movie>> ListAsync(string movieGenre, string searchString)
        {
           
            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return movies.ToListAsync();
        }

        public Task<Movie> FindAsync(int id)
        {
            return _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
        }

        public bool Exists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }

        public IQueryable<string> GenreList()
        {
            return from m in _context.Movie
                   orderby m.Genre
                   select m.Genre;
        }

        public Task Add(Movie movie)
        {
            _context.Movie.Add(movie);
            return _context.SaveChangesAsync();
        }

        public Task Remove(Movie movie)
        {
            _context.Movie.Remove(movie);
            return _context.SaveChangesAsync();
        }

        public Task Update(Movie movie)
        {
            _context.Movie.Update(movie);
            return _context.SaveChangesAsync();
        }
    }
}
