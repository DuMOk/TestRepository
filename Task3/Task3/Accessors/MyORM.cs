using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlServerCe;
using System.Configuration;
using System.Reflection;

namespace Task3
{
    class MyORM : IPersonAccessors
    {

        public object obj = new object();

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
            var attTbl = GetTableName();
            string qrSQL = string.Format("Select * From {0}", attTbl.TableName);
            SqlCeCommand SQLcmd = new SqlCeCommand(qrSQL, connect);
            SqlCeDataReader Reader = SQLcmd.ExecuteReader();
            FieldInfo[] Fld = obj.GetType().GetFields();
            for (int i = 0; i < Fld.Length; i++)
            {
                var attrib = GetFieldNameType(Fld[i].Name);
                Console.Write("  {0}  ", attrib.FieldName);
            }
            Console.WriteLine();
            while (Reader.Read())
            {
                for (int i = 0; i < Fld.Length; i++)
                {
                    var attrib = GetFieldNameType(Fld[i].Name);
                    if (attrib.FieldType == "int")
                        Console.Write("      {0:d2}   |", Reader[attrib.FieldName]);
                    else
                        Console.Write(" {0} ", Reader[attrib.FieldName]);
                }
                Console.WriteLine();
            }
        }

        public string GetByName(string name)
        {

            var attTbl = GetTableName();
            FieldInfo[] Fld = obj.GetType().GetFields();
            string nameFld = null;
            for (int i = 0; i < Fld.Length; i++)
            {
                var attrib = GetFieldNameType(Fld[i].Name);
                if (attrib.FieldType == "string")
                    nameFld = attrib.FieldName;
            }
            string qrSQL = string.Format("Select * From {0} Where Lower({1}) = Lower('{2}')", attTbl.TableName, nameFld, name);
            SqlCeCommand SQLcmd = new SqlCeCommand(qrSQL, connect);
            SqlCeDataReader Reader = SQLcmd.ExecuteReader();       
            if (Reader.Read())
            {
                return Reader[nameFld].ToString();
            }
            else
            {
                Console.WriteLine("Person with name {0} does not exist!", name);
                return null;
            } 
        }

        public void DeleteByName(string name)
        {
            var attTbl = GetTableName();
            FieldInfo[] Fld = obj.GetType().GetFields();
            string nameFld = null;
            for (int i = 0; i < Fld.Length; i++)
            {
                var attrib = GetFieldNameType(Fld[i].Name);
                if (attrib.FieldType == "string")
                    nameFld = attrib.FieldName;
            }
            string sqlDel = string.Format("Delete From {0} Where Lower({1}) = Lower('{2}')", attTbl.TableName, nameFld, name);
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
                Exception error = new Exception("Error", ex);
                throw error;
            }
        }

        public void Insert(string name)
        {
            var attTbl = GetTableName();
            FieldInfo[] Fld = obj.GetType().GetFields();
            string nameIntFld = null;
            string nameStrFld = null;
            for (int i = 0; i < Fld.Length; i++)
            {
                var attrib = GetFieldNameType(Fld[i].Name);
                if (attrib.FieldType == "int")
                    nameIntFld = attrib.FieldName;
                else
                    nameStrFld = attrib.FieldName;
            }
            string sql = string.Format("Select MAX({0}) as max From {1}", nameIntFld, attTbl.TableName);
            SqlCeCommand SQLcmd = new SqlCeCommand(sql, connect);
            SqlCeDataReader Reader = SQLcmd.ExecuteReader();
            int ind = 0;
            while (Reader.Read())
            {
                ind = (int)Reader["max"];
            }
            string sqlIns = string.Format("Insert Into {0}({1}, {2}) Values({3}, '{4}')", attTbl.TableName, nameIntFld, nameStrFld, ind + 1, name);
            SqlCeTransaction tx = null;
            tx = connect.BeginTransaction();
            SQLcmd = new SqlCeCommand(sqlIns, connect);
            SQLcmd.Transaction = tx;
            SQLcmd.ExecuteNonQuery();
            tx.Commit();
        }
        
        private TableAttribute GetTableName()
        {
            return Attribute.GetCustomAttribute(obj.GetType(), typeof(TableAttribute)) as TableAttribute;
        }

        private FieldAttribute GetFieldNameType(string fld)
        {
            return Attribute.GetCustomAttribute(obj.GetType().GetField(fld), typeof(FieldAttribute)) as FieldAttribute;
        }
    }
}
