﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Web.Data;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly CinemaContext _context;

        public MoviesController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Movies
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string sortOrder, string searchString, int pageIndex = 1)
        {
            int pageSize = 2;
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.ReleaseDateSortParam = sortOrder == "release_date" ? "release_date_desc" : "release_date";
            ViewBag.CurrentFilter = searchString;
            IQueryable<Movie> movies = _context.Movie;

            if(!string.IsNullOrWhiteSpace(searchString))
            {
                movies = movies.Where(x => x.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc": movies = movies.OrderByDescending(x => x.Title);
                    break;
                case "release_date": movies = movies.OrderBy(x => x.ReleaseDate);
                    break;
                case "release_date_desc": movies = movies.OrderByDescending(x => x.ReleaseDate);
                    break;
                default:
                    movies = movies.OrderBy(x => x.Title);
                    break;
            }

            var paginatedList = await PaginatedList<Movie>.CreateAsync(movies.Include(m => m.Producer), pageIndex, pageSize);

            return View(paginatedList);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Producer)
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["ProducerId"] = new SelectList(_context.Producer, "Id", "FirstName");
            ViewData["ActorId"] = new MultiSelectList(_context.Actor, "Id", "FirstName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ReleaseDate,Price,Genre,Rating,ProducerId,Id,Actors")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                var isId = int.TryParse(ModelState["Actors"].RawValue as string, out int id);
                if (isId)
                {
                    movie.Actors = _context.Actor.Where(x => id == x.Id).ToList();
                }
                else
                {
                    var ids = (ModelState["Actors"].RawValue as string[]).Select(x => int.Parse(x));
                    movie.Actors = _context.Actor.Where(x => ids.Contains(x.Id)).ToList();
                }

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ProducerId = new SelectList(_context.Producer, "Id", "FirstName", movie.ProducerId);

            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["ProducerId"] = new SelectList(_context.Producer, "Id", "FirstName", movie.ProducerId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,ReleaseDate,Price,Genre,Rating,ProducerId,Id")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
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
            ViewData["ProducerId"] = new SelectList(_context.Producer, "Id", "FirstName", movie.ProducerId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
