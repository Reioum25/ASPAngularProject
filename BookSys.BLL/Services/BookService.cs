using BookSys.BLL.Helpers;
using BookSys.DAL;
using BookSys.DAL.Models;
using BookSys.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookSys.BLL.Services
{
    public class BookService: IGenericService<BookVM,long>
    {
        private ToViewModel toViewModel = new ToViewModel();
        private ToModel toModel = new ToModel();
        private readonly BookSysContext context;
        public BookService(BookSysContext _context)
        {
            context = _context;
        }

        public ResponseVM Create (BookVM bookVM)
        {
            using (context)
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                    try
                    {
                        bookVM.MyGuid = Guid.NewGuid();
                         context.Books.Add(toModel.Book(bookVM));
                        context.SaveChanges();

                        //commit change to db
                        dbTransaction.Commit();
                        return new ResponseVM("create", true, "Book");
                    }
                    catch (Exception ex)
                    {
                        //rollback changes
                        dbTransaction.Rollback();
                        return new ResponseVM("create", false, "Book", ResponseVM.SOMETHING_WENT_WRONG, "", ex);
                        
                    }
            }
        }

        public ResponseVM Delete (long id)
        {
            using (context)
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Book bookToBeDeleted = context.Books.Find(id);
                        if (bookToBeDeleted == null)
                            return new ResponseVM("deleted", false, "Book", ResponseVM.DOES_NOT_EXIST);

                        // delete from database
                        context.Books.Remove(bookToBeDeleted);
                        context.SaveChanges();

                        dbTransaction.Commit();
                        return new ResponseVM("deleted", true, "Book");
                    }
                    catch (Exception ex)
                    {
                        //rollback changes
                        dbTransaction.Rollback();
                        return new ResponseVM("deleted", false, "Book", ResponseVM.SOMETHING_WENT_WRONG, "", ex);
                    }
                }
            }
        }


        public IEnumerable<BookVM> GetAll()
        {
            using (context)
            {
                try
                {
                    var books = context.Books.ToList().OrderByDescending(x => x.ID);
                    var booksVm = books.Select(x => toViewModel.Book(x));
                    return booksVm;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        public BookVM GetSingleBy(long id)
        {
            using (context)
            {
                try
                {
                    // select * from books where id = 'id'
                    var book = context.Books.Find(id);
                    // pag gusto specific column by title ng book ito ung itype
                    //var vooksTitle = context.Books.Where(x => x.Title == "");
                    BookVM bookVM = null;
                    if (book != null) bookVM = toViewModel.Book(book);
                    return bookVM;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public ResponseVM Update(BookVM bookVM)
        {
            using (context)
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //find book from the databse
                        Book bookToBeUpdated = context.Books.Find(bookVM.ID);
                        if (bookToBeUpdated == null)
                            return new ResponseVM("updated", false, "Book", ResponseVM.DOES_NOT_EXIST);

                        // update changes
                        bookToBeUpdated.Title = bookVM.Title;
                        bookToBeUpdated.Copyright = bookVM.Copyright;
                        context.SaveChanges();
                        dbTransaction.Commit();
                        return new ResponseVM("updated", true, "Book");
                    }
                    catch (Exception ex)
                    {

                        dbTransaction.Rollback();
                        return new ResponseVM("updated", false, "Book", ResponseVM.SOMETHING_WENT_WRONG, "", ex);
                    }
                }
            }
        }
    }
}
