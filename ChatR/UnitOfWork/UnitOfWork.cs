using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatR.Entities;

namespace ChatR.UnitOfWork
{
    public class UnitOfWork //: IDisposable
    {
        //private DBChatREntities _context = new DBChatREntities();

        //private GenericRepository<User> _userRepository;
        //private GenericRepository<Message> _messageRepository;

        //public GenericRepository<User> UserRepository 
        //{
        //    get 
        //    {
        //        if (_userRepository == null)
        //        {
        //            _userRepository = new GenericRepository<User>(_context);
        //        }

        //        return _userRepository;
        //    }
        //}

        //public GenericRepository<Message> MessageRepository
        //{
        //    get 
        //    {
        //        if (_messageRepository == null)
        //        {
        //            _messageRepository = new GenericRepository<Message>(_context);
        //        }
        //        return _messageRepository;
        //    }
        //}

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}