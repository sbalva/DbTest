using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data;
using BLToolkit.DataAccess;

namespace DBTest
{
    public class DBConnector
    {
        #region Constructor
        public DBConnector(string server, string database, string user, string password)
            : this(String.Format("MultipleActiveResultSets=True;Persist Security Info=True;User ID={0};Initial Catalog={1};Data Source={2};Password={3};Connection Timeout=3600",
                user, database, server, password))
        { 
        }        

        public DBConnector(string connectionString)
        { 
            if (!String.IsNullOrEmpty(connectionString))
                DbManager.AddConnectionString(connectionString);
        }
        #endregion

        public bool TestConnection()
        {
            string sql_TestConnection = "SELECT 1";
            using (DbManager db = new DbManager())
            {
                db.SetCommand(sql_TestConnection);
                object obj = db.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                {
                    if (Convert.ToInt16(obj) == 1)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public int Insert<T>(T obj) where T : class
        {
            SqlQuery<T> query = new SqlQuery<T>();
            return query.Insert(obj);
        }

        public int Update<T>(T obj) where T : class
        {
            SqlQuery<T> query = new SqlQuery<T>();
            return query.Update(obj);
        }

        public T SelectByKey<T>(object key) where T : class
        {
            return new SqlQuery<T>().SelectByKey(key);
        }
    }
}
