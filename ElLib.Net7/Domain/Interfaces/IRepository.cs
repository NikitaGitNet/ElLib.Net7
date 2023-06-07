using System;
using System.Collections.Generic;

namespace ElLib.Net7.Domain.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Save(T entity);
        void Delete(Guid id);
        void DeleteRange(string id);
        T GetByCodeWord(string codeWord);
    }
}
