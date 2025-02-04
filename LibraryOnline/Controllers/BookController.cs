using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryOnline.Data;
using LibraryOnline.Models;
using LibraryOnline.Books;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryOnline.Controllers;


[Route("api/[controller]")]
[ApiController]

public class BookController : ControllerBase
{
    private readonly AppDbContext context;
    private object _context;

    public BookController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<books>>> Getbooks()
    {
        var books = await context.books.ToListAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<books>> GetBooks(int id)
    {
        var book = await context.books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<books>> Postbooks(books books)
    {
        if (books == null)
        {
            return BadRequest("Invalid Book");
        }

        _context.books.Add(books);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBooks), new {id = books.Id}, books);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> PutBook(int id, books books) 
    {
        if (id != books.Id)
        {
            return BadRequest();
        }

        _context.Entry(books).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) 
        {
            if (!BookExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id) 
    {
        var books = await _context.books.FindAsync(id);
        if (books == null) 
        {
            return NotFound();
        }

        _context.books.Remove(books);
        await _context.SaveChangesAsync();
        return NoContent();

    }

    private bool BookExists(int id)
    {
        return _context.books.Any(e => e.Id == id);
    }
}

