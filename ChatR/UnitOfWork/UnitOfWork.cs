using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatR.Entities;
using ChatR.Models;

namespace ChatR.UWork
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        private GenericRepository<Message> _messageRepository;
        private GenericRepository<ApplicationUser> _userRepository;

        public GenericRepository<Message> MessageRepository
        {
            get
            {
                if (_messageRepository == null)
                {
                    _messageRepository = new GenericRepository<Message>(_context);
                }
                return _messageRepository;
            }
        }

        public GenericRepository<ApplicationUser> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<ApplicationUser>(_context);
                }
                return _userRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}