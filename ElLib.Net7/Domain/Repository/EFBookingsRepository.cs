using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElLib.Net7.Domain.Repository
{
    public class EFBookingsRepository : IRepository<Booking>
    {
        private readonly AppDbContext context;
        public EFBookingsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Booking> GetAll()
        {
            return context.Bookings;
        }
        public Booking GetById(Guid id)
        {
            return context.Bookings.FirstOrDefault(x => x.Id == id);
        }
        public void Save(Booking entity)
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
            context.Bookings.Remove(new Booking() { Id = id });
            context.SaveChanges();
        }
        public void DeleteRange(string id)
        {
            IEnumerable<Booking> bookings = GetAll();
            var sortBookings = from booking in bookings where booking.UserId == id select booking;
            context.Bookings.RemoveRange(sortBookings);
            context.SaveChanges();
        }
        public Booking GetByCodeWord(string codeWord)
        {
            throw new NotImplementedException();
        }
    }
}
