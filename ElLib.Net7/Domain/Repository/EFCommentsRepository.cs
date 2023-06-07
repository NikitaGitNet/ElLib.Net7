using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElLib.Net7.Domain.Repository
{
    public class EFCommentsRepository : IRepository<Comment>
    {
        private readonly AppDbContext context;
        public EFCommentsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Comment> GetAll()
        {
            return context.Comments;
        }
        public Comment GetById(Guid id)
        {
            return context.Comments.FirstOrDefault(x => x.Id == id);
        }
        public void Save(Comment entity)
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
            context.Comments.Remove(new Comment() { Id = id });
            context.SaveChanges();
        }
        public void DeleteRange(string id)
        {
            IEnumerable<Comment> cooments = GetAll();
            var sortCooments = from comment in cooments where comment.UserId == id select comment;
            context.Comments.RemoveRange(sortCooments);
            context.SaveChanges();
        }
        public Comment GetByCodeWord(string codeWord)
        {
            throw new NotImplementedException();
        }
    }
}
