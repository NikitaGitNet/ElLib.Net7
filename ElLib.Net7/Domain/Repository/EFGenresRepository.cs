using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElLib.Net7.Domain.Repository
{
    public class EFGenresRepository : IRepository<Genre>
    {
        private readonly AppDbContext context;
        public EFGenresRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Genre> GetAll()
        {
            return context.Genres;
        }
        public Genre GetById(Guid id)
        {
            return context.Genres.FirstOrDefault(x => x.Id == id);
        }
        public void Save(Genre entity)
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
            context.Genres.Remove(new Genre() { Id = id });
            context.SaveChanges();
        }
        public void DeleteRange(string id)
        {
            throw new NotImplementedException();
        }
        public Genre GetByCodeWord(string codeWord)
        {
            throw new NotImplementedException();
        }
    }
}
