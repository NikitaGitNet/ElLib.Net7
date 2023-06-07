using ElLib.Net7.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using ElLib.Net7.Domain.Interfaces;

namespace ElLib.Net7.Domain.Repository
{
    public class EFAuthorsRepository : IRepository<Author>
    {
        private readonly AppDbContext context;
        public EFAuthorsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Author> GetAll()
        {
            return context.Authors;
        }
        public Author GetById(Guid id)
        {
            return context.Authors.FirstOrDefault(x => x.Id == id);
        }
        public void Save(Author entity)
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
            context.Authors.Remove(new Author() { Id = id });
            context.SaveChanges();
        }
        public void DeleteRange(string id)
        {
            throw new NotImplementedException();
        }
        public Author GetByCodeWord(string codeWord)
        {
            throw new NotImplementedException();
        }
    }
}
