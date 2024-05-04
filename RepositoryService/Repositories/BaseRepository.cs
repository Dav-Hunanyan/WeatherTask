using Weather.Dal.Models;

namespace RepositoreService.Repositories
{
    public class BaseRepository:IDisposable
    {
        #region Props
        private LigadatabaseContext? db;
        public LigadatabaseContext? DB { get { return db = db ?? new LigadatabaseContext(); } }
        #endregion

        #region Constructors
        public BaseRepository() { }
        #endregion

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
