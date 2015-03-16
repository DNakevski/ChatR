using ChatR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatR.UWork;

namespace ChatR.WorkerService
{
    public class BaseWorkerService : IDisposable
    {
        protected readonly ApplicationDbContext _context = new ApplicationDbContext();
        private UnitOfWork _unitOfWork;
       
        protected UnitOfWork UnitOfWork { 
            get 
            { 
                if(_unitOfWork == null)
                {
                    _unitOfWork = new UnitOfWork();
                }

                return _unitOfWork;
            }
        }


        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    UnitOfWork.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}