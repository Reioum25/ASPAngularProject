using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSys.BLL.Services;
using BookSys.ViewModel.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IGenericService<BookVM, long> bookService;

        public BookController(BookService _bookService)
        {
            bookService = _bookService;
        }

        //api/Book/Create

        [HttpPost("[action]")]
        public ActionResult<ResponseVM> Create([FromBody]BookVM bookVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong!");
                
            }
            return bookService.Create(bookVM);
        }

        //api/Book/Delete

        [HttpDelete("[action]/{id}")]
        public ActionResult<ResponseVM> Delete(long id)
        {
            return bookService.Delete(id);
        }

        //api/Book/GetAll

        [HttpGet("[action]")]
        public IEnumerable<BookVM> GetAll()
            {
            return bookService.GetAll();

            }


        //api/Book/GetSingleBy

        [HttpGet("[action]/{id}")]
        public BookVM GetSingleBy(long id)
        {
            return bookService.GetSingleBy(id);

        }

        //api/Book/Update

        [HttpPut("[action]")]
        public ActionResult<ResponseVM>Update([FromBody] BookVM bookVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong!");

            }
            return bookService.Update(bookVM);
        }

    }
}