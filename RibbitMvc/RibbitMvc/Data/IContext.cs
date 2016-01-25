using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RibbitMvc.Data
{
    public interface IContext : IDisposable
    {
        IUserRepository Users { get; }
        IRibbitRepository Ribbits { get; }
        IUserProfileRepository Profiles { get; }

        int SaveChanges();
    }
}