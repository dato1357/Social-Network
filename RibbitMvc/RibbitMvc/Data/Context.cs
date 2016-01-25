using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RibbitMvc.Data
{
    public class Context : IContext
    {
        private readonly DbContext _db;

        public Context(DbContext context = null, IUserRepository users = null, 
            IRibbitRepository ribbits = null, IUserProfileRepository profiles = null)
        {
            _db = context ?? new RibbitDatabase();
            Users = users ?? new UserRepository(_db, true);
            Ribbits = ribbits ?? new RibbitRepository(_db, true);
            Profiles = profiles ?? new UserProfileRepository(_db, true);
        }

        public IUserRepository Users
        {
            get;
            private set;
        }

        public IRibbitRepository Ribbits
        {
            get;
            private set;
        }

        public IUserProfileRepository Profiles { get; private set; }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                try
                {
                    _db.Dispose();
                }
                catch { }
            }
        }
    }
}