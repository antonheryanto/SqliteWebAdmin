using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace SqliteWebAdmin.Models
{
    public class Query
    {        
        public string File { get; set; }
        public string Sql { get; set; }
        public IEnumerable<IDictionary<string, object>> Result { get; set; }
        public string Message { get; set; }

        public DbConnection Db()  
        {        
            if (!System.IO.File.Exists(File)) return null;
            var cn = new System.Data.SQLite.SQLiteConnection(string.Format("Data Source={0};Version=3;", File));
            cn.Open();
            return cn;
        }        
    }

    public struct Sql
    {
        public const string TABLES = @"SELECT name FROM sqlite_master WHERE type='table' ORDER BY name";
    }
}