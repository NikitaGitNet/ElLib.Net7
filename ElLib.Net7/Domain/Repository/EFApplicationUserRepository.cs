using ElLib.Net7.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ElLib.Net7.Domain.Interfaces;

namespace ElLib.Net7.Domain.Repository
{
    public class EFApplicationUserRepository : IRepository<ApplicationUser>
    {
        private readonly AppDbContext context;
        public EFApplicationUserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return context.ApplicationUsers;
        }
        public ApplicationUser GetById(Guid id)
        {
            string userId = id.ToString();
            return context.ApplicationUsers
                .Include(x => x.Comments)
                .Include(x => x.Bookings)
                .FirstOrDefault(x => x.Id == userId);

        }
        public void Save(ApplicationUser entity)
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
            string userId = id.ToString();
            context.ApplicationUsers.Remove(new ApplicationUser() { Id = userId });
            context.SaveChanges();
        }
        public void DeleteRange(string id)
        {
            throw new NotImplementedException();
        }
        public ApplicationUser GetByCodeWord(string codeWord)
        {
            throw new NotImplementedException();
        }
    }
}
