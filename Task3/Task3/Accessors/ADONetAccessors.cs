using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlServerCe;
using System.Configuration;

namespace Task3
{
    public class ADONetAccessors : IPersonAccessors
    {
        private SqlCeConnection connect = null;

        public void OpenConnection()
        {
            connect = new SqlCeConnection();
            connect.ConnectionString = @"Data Source=..\..\DB\PersonDB.sdf; Password = 123456";            
            connect.Open();
        }

        public void CloseConnection()
        {
            connect.Close();
        }

        public void GetAll()
        {
            Console.WriteLine("Id Person | Name Person");
            string qrSQL = "Select * From Person";
            SqlCeCommand SQLcmd = new SqlCeCommand(qrSQL, connect);
            SqlCeDataReader Reader = SQLcmd.ExecuteReader();
            while (Reader.Read())
            {
                Console.WriteLine("     {0:d2}   | {1}", Reader["IdPerson"], 
                                                    Reader["NamePerson"].ToString());
            }
        }

        public string GetByName(string name)
        {
            string qrSQL = string.Format("Select * From Person Where NamePerson = '{0}'", name);
            SqlCeCommand SQLcmd = new SqlCeCommand(qrSQL, connect);
            SqlCeDataReader Reader = SQLcmd.ExecuteReader();
            if (Reader.Read())
            {
                return Reader["NamePerson"].ToString();
            }
            else
            {
                Console.WriteLine("Person with name {0} does not exist!", name);
                return null;
            } 
        }

        public void DeleteByName(string name)
        {
            string sqlDel = string.Format("Delete From Person Where NamePerson = '{0}'", name);
            SqlCeCommand SQLcmd = new SqlCeCommand(sqlDel, connect);
            try
            {
                SqlCeTransaction tx = null;
                tx = connect.BeginTransaction();
                SQLcmd.Transaction = tx;
                SQLcmd.ExecuteNonQuery();
                tx.Commit();
            }
            catch (SqlCeException ex)
            {
                Exception error = new Exception("Error",ex);
                throw error;
            }

        }

        public void Insert(string name)
        {
            string sql = string.Format("Select MAX(IdPerson) as max From Person");
            SqlCeCommand SQLcmd = new SqlCeCommand(sql, connect);
            SqlCeDataReader Reader = SQLcmd.ExecuteReader();
            int ind = 0;
            while (Reader.Read())
            {
                ind = (int)Reader["max"];
            }
            
            string sqlIns = string.Format("Insert Into Person(IdPerson, NamePerson)" + 
                                          "Values({1}, '{0}')", name, ind + 1);
            SqlCeTransaction tx = null;
            tx = connect.BeginTransaction();
            SQLcmd = new SqlCeCommand(sqlIns, connect);
            SQLcmd.Transaction = tx;
            SQLcmd.ExecuteNonQuery();
            tx.Commit();
        }
    }
}
