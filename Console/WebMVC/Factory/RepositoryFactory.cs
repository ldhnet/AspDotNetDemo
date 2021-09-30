﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Model;

namespace WebMVC.Context
{
    public class RepositoryFactory
    {
        public RepositoryBase BaseRepository()
        {
            IDatabase database = null;
            ApplicationDbContext dbContext=new ApplicationDbContext();
            string dbType = GlobalContext.SystemConfig.DBProvider; 
            switch (dbType)
            {
                case "SqlServer": 
                    database = new SqlServerDatabase(dbContext);
                    break;
                case "MySql":  
                    break;
                case "Oracle": 
                    break;
                default:
                    throw new Exception("未找到数据库配置");
            }
            return new RepositoryBase(database);
        }
    }
}