using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Context
{
    public class RepositoryBase
    {
        public IDatabase db;

        public RepositoryBase(IDatabase iDatabase)
        {
            this.db = iDatabase;
        } 
        public T FindEntity<T>(int id) where T : class
        {
            return  db.FindEntity<T>(id);
        }
    }
}
