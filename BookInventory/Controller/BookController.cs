using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;



namespace BookInventoryApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
         private readonly iBookServices bookService;


         public BookController(iBookServices bookService)
         {
            this.bookService = bookService;
         }

         [HttpGet]
            public async Task<ActionResult<List<Book>>> GetBookAsync() => Ok(await bookService.GetBookAsync());

           [HttpPost]
           public async Task<ActionResult<(bool,string)>> ManageBookAsync(Book book)
           {
               if(book is null) return BadRequest(false);

               var result = await bookService.ManageBookAsync(book);

               if(result.Success) return Ok(result);
               else return BadRequest(result);
           }

             [HttpDelete("{id:int}")]
             public async Task<ActionResult<(bool,string)>> DeleteBookAsync(int id)
             {
                   var result = await bookService.DeleteBookAsync(id);

                   if(result.Success) return Ok(result);
                   else return  BadRequest(result);
              }
}
}