using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElLib.Net7.Domain.Repository
{
    public class EFBooksRepository : IRepository<Book>
    {
        private readonly AppDbContext context;
        public EFBooksRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Book> GetAll()
        {
            return context.Books;
        }
        public Book GetById(Guid id)
        {
            return context.Books
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.Id == id);
        }
        public void Save(Book entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            context.Books.Remove(new Book() { Id = id });
            context.SaveChanges();
        }
        public void DeleteRange(string id)
        {
            throw new NotImplementedException();
        }
        public Book GetByCodeWord(string codeWord)
        {
            throw new NotImplementedException();
        }
    }
}
