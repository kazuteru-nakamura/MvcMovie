using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Core.Interfaces;
using MvcMovie.Core.Model;
using MvcMovie.Infrastructure;
using MvcMovie.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {

        private readonly IMovieRepository _movieRepository;

        public MoviesController(MvcMovieContext context, IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            var genreQuery = _movieRepository.GenreList();

            var movies = _movieRepository.ListAsync(movieGenre, searchString);

            var movieVM =  new List<MovieViewModel>();

            foreach (var item in await movies)
            {
                movieVM.Add(this.CreateMovieViewModel(item));
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = movieVM
            };

            return View(movieGenreVM);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieRepository.FindAsync((int)id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(this.CreateMovieViewModel(movie));
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieRepository.Add(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(this.CreateMovieViewModel(movie));
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieRepository.FindAsync((int)id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(this.CreateMovieViewModel(movie));
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _movieRepository.Update(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(this.CreateMovieViewModel(movie));
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var movie = await _context.Movie
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var movie = await _movieRepository.FindAsync((int)id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(this.CreateMovieViewModel(movie));
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _movieRepository.FindAsync((int)id);

            await _movieRepository.Remove(movie);

            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _movieRepository.Exists(id);
        }

        private MovieViewModel CreateMovieViewModel(Movie model)
        {
            return new MovieViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseDate = model.ReleaseDate,
                Genre = model.Genre,
                Rating = model.Rating,
                Price = model.Price
            };
        }
    }
}
