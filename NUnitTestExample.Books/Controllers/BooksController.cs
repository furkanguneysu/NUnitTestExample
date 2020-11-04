using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnitTestExample.Books.Database;
using NUnitTestExample.Books.Database.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NUnitTestExample.Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        DatabaseContext dbContext;
        public BooksController()
        {
            dbContext = new DatabaseContext();
        }
        // GET: api/<BooksController>
        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            List<Book> books = await dbContext.Books.ToListAsync();
            
            if(!books.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(books);
            }

        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var searchResult = await dbContext.Books.FindAsync(id);
            if(searchResult == null)
            {
                return  NotFound();
            }
            else
            {
                return Ok(searchResult);
            }
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            try
            {
                await dbContext.AddAsync(book);
                await dbContext.SaveChangesAsync();
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Book book)
        {
            try
            {
                if(id != book.Id)
                {
                    return BadRequest();
                }

                var searchedBook = await dbContext.Books.FindAsync(id);

                if(searchedBook == null)
                {
                    return NotFound();
                }
                else
                {
                    searchedBook.Name = book.Name;
                    searchedBook.Author = book.Author;
                    searchedBook.Price = book.Price;
                    await dbContext.SaveChangesAsync();
                    return Ok(book);
                }
            }


            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var searchResult = await dbContext.Books.FindAsync(id);
                if(searchResult == null)
                {
                    return NotFound();
                }
                else
                {
                    dbContext.Books.Remove(searchResult);
                    await dbContext.SaveChangesAsync();
                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex);
            }
        }
    }
}
