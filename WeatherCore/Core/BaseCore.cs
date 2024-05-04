using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Dal.Models;

namespace WeatherCore.Core
{
    public abstract class BaseCore : IDisposable
    {
        private LigadatabaseContext? db;
        public LigadatabaseContext? DB { get { return db; } }

        public BaseCore(LigadatabaseContext db)
        {
            this.db = db;
        }

        #region Dispose
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (db != null)
                    {
                        db.Dispose();
                        db = null;
                    }
                }
                disposed = true;
            }
        }
        #endregion
    }
}
