﻿using BookSys.BLL.Services;
using BookSys.DAL.Models;
using BookSys.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSys.BLL.Helpers
{
    public class ToModel
    {
        public Book Book(BookVM bookVM)
        {
            return new Book
            {
                ID = bookVM.ID,
                MyGuid = bookVM.MyGuid,
                Title = bookVM.Title,
                Copyright = bookVM.Copyright

            };
        }
    }
}
