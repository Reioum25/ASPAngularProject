using BookSys.ViewModel.ViewModel;
using System;
using System.Collections.Generic;

namespace BookSys.BLL.Services
{
    public interface IGenericService<TVM, TType> where TVM : class where TType : IConvertible
    {
        IEnumerable<TVM> GetAll();
        TVM GetSingleBy(long id);
        ResponseVM Create(TVM entity);
        ResponseVM Delete(TType guid);
        ResponseVM Update(TVM entity);
    }
}