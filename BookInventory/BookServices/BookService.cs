using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//

namespace BookInventoryApp
{
    public class BookService : iBookServices
    {
        private readonly AppDbContext appDbContext;

        public BookService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<(bool Success, string Message)> ManageBookAsync(Book book)
        {
           if(book is null){
            return ErrorMessage();
           }


        if(book.Id ==0){
           if(!await IsBookAlreadyAdded(book.Title))
           {
               appDbContext.BooksTable.Add(book);
                await appDbContext.SaveChangesAsync();
                return SuccessMessage();
           }
            }
            var bookToUpdate = await appDbContext.BooksTable.FirstOrDefaultAsync(_ => _.Id == book.Id);

            if(bookToUpdate is null){
                return SuccessMessage();
            }


            bookToUpdate.Title = book.Title;
            bookToUpdate.Description = book.Description;
            bookToUpdate.Image = book.Image;

            await appDbContext.SaveChangesAsync();
            return SuccessMessage();
        }

        public async Task<(bool Success, string Message)> DeleteBookAsync(int id)
        {
          if(id <= 0) return ErrorMessage();

          var bookToDelete = await appDbContext.BooksTable.FirstOrDefaultAsync(_ => _.Id  == id);
         if(bookToDelete == null){
              return ErrorMessage();
           }
           else{
                    appDbContext.BooksTable.Remove(bookToDelete);

                     await appDbContext.SaveChangesAsync();

                    return SuccessMessage();
           }
           
        }

        public async Task<List<Book>> GetBookAsync() => 
         await appDbContext.BooksTable.ToListAsync();
           private static (bool,string) SuccessMessage() => (true, "Process successfully completed");

           private static (bool,string) ErrorMessage() => (false, "Error occurred while processing");
    

        public async Task<bool> IsBookAlreadyAdded(string bookName)
        {
            var book = await appDbContext.BooksTable.Where(_ => _.Title!.ToLower().Equals(bookName)).FirstOrDefaultAsync();
            return book is not null;
        }


        }
    }
