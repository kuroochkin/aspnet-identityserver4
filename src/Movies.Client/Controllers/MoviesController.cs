﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.ApiServices;
using Movies.Client.Models;

namespace Movies.Client.Controllers;

[Authorize]
public class MoviesController : Controller
{
    private readonly IMovieApiService _movieApiService;

    public MoviesController(IMovieApiService movieApiService)
    {
        _movieApiService = movieApiService;
    }
    
    // GET: Movies
    public async Task<IActionResult> Index()
    {
        return View(await _movieApiService.GetMovies());
    }
    
    // GET: Movies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        return View();
        
        // if (id == null)
        //     return NotFound();
        //
        // var movie = await _context.Movie
        //     .FirstOrDefaultAsync(m => m.Id == id);
        //
        // if (movie == null)
        //     return NotFound();
        //
        // return View(movie);
    }
    
    // GET: Movies/Create
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: Movies/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] Movie movie)
    {
        // if (!ModelState.IsValid)
            return View(movie);
        
        // _context.Add(movie);
        // await _context.SaveChangesAsync();
        // return RedirectToAction(nameof(Index));
    }
    
    // GET: Movies/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        return View();
        // if (id == null)
        //     return NotFound();
        //
        // var movie = await _context.Movie.FindAsync(id);
        //
        // if (movie == null)
        //     return NotFound();
    }
    
    // POST: Movies/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] Movie movie)
    {
        return View();
        
        // if (id != movie.Id)
        //     return NotFound();
        //
        // if (!ModelState.IsValid) 
        //     return View(movie);
        //
        // try
        // {
        //     _context.Update(movie);
        //     await _context.SaveChangesAsync();
        // }
        // catch (DbUpdateConcurrencyException)
        // {
        //     if (!MovieExists(movie.Id))
        //         return NotFound();
        // }
        //     
        // return RedirectToAction(nameof(Index));
    }
    
    // GET: Movies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        return View();
        
        // if (id == null)
        //     return NotFound();
        //
        // var movie = await _context.Movie
        //     .FirstOrDefaultAsync(m => m.Id == id);
        //
        // if (movie == null)
        //     return NotFound();
        //
        // return View(movie);
    }
    
    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        return View();

        // var movie = await _context.Movie.FindAsync(id);
        // _context.Movie.Remove(movie);
        // await _context.SaveChangesAsync();
        // return RedirectToAction(nameof(Index));
    }

    public async Task Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
    }
    
    private bool MovieExists(int id)
    {
        return true;
        // return _context.Movie.Any(e => e.Id == id);
    }
}