using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookInventoryApp
{
    public interface iBookServices
    {
        Task<(bool Success, string Message)> ManageBookAsync(Book book);

        Task<(bool Success, string Message)> DeleteBookAsync(int id);

        Task<List<Book>> GetBookAsync();
    }
}
