using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Data.SqlServerCe;
using NLog;

namespace Accessors
{
    public class MyORM<T> : IDataAccessors<T>
    {
        private SqlCeConnection connect = null;
        private Logger myLog = LogManager.GetCurrentClassLogger();

        public void OpenConnection(string connString)
        {
            try
            {
                connect = new SqlCeConnection();
                connect.ConnectionString = connString;
                connect.Open();
            }
            catch (SqlCeException invOp)
            {
                myLog.Error(invOp);
                throw;
            }
        }

        public void CloseConnection()
        {
            connect.Close();
        }

        public List<T> GetAll()
        {
            var attTbl = GetTableName();
            string qrSQL = string.Format("Select * From {0}", attTbl.TableName);
            var records = new List<T>();

            using (SqlCeCommand SQLcmd = new SqlCeCommand(qrSQL, connect))
            {
                SqlCeDataReader Reader = SQLcmd.ExecuteReader();
                FieldInfo[] Fld = typeof(T).GetFields();
            
                while (Reader.Read())
                {
                    var constructor = typeof(T).GetConstructor(new Type[0]);
                    var item = (T)constructor.Invoke(new object[0]);
                    
                    for (int i = 0; i < Fld.Length; i++)
                    {
                        var attrib = GetFieldNameType(Fld[i].Name);
                        item.GetType().GetField(Fld[i].Name).SetValue(item, Reader[attrib.FieldName]);
                    }
                    
                    records.Add(item);
                }
            }

            return records;
        }

        public T GetByName(string name)
        {
            var attTbl = GetTableName();
            FieldInfo[] Fld = typeof(T).GetFields();
            string nameFld = null;
            
            for (int i = 0; i < Fld.Length; i++)
            {
                var attrib = GetFieldNameType(Fld[i].Name);
                if (attrib.FieldType == "string")
                    nameFld = attrib.FieldName;
            }

            string qrSQL = string.Format("Select * From {0} Where Lower({1}) = Lower('{2}')", attTbl.TableName, nameFld, name);
            
            using (SqlCeCommand SQLcmd = new SqlCeCommand(qrSQL, connect))
            {
                SqlCeDataReader Reader = SQLcmd.ExecuteReader();
                if (Reader.Read())
                {
                    var constructor = typeof(T).GetConstructor(new Type[0]);
                    var item = (T)constructor.Invoke(new object[0]);

                    for (int i = 0; i < Fld.Length; i++)
                    {
                        var attrib = GetFieldNameType(Fld[i].Name);
                        item.GetType().GetField(Fld[i].Name).SetValue(item, Reader[attrib.FieldName]);
                    }

                    return item;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public void DeleteByName(string name)
        {
            var attTbl = GetTableName();
            FieldInfo[] Fld = typeof(T).GetFields();
            string nameFld = null;
            
            for (int i = 0; i < Fld.Length; i++)
            {
                var attrib = GetFieldNameType(Fld[i].Name);
                if (attrib.FieldType == "string")
                    nameFld = attrib.FieldName;
            }
            
            string sqlDel = string.Format("Delete From {0} Where Lower({1}) = Lower('{2}')", attTbl.TableName, nameFld, name);
            
            using (SqlCeCommand SQLcmd = new SqlCeCommand(sqlDel, connect))
            {
                SqlCeTransaction tx = null;
                tx = connect.BeginTransaction();
                try
                {
                    SQLcmd.Transaction = tx;
                    SQLcmd.ExecuteNonQuery();
                    tx.Commit();
                }
                catch (SqlCeException invOp)
                {
                    myLog.Error(invOp);
                    tx.Rollback();
                    throw;
                }
            }
        }

        public void Insert(T name)
        {
            var attTbl = GetTableName();
            FieldInfo[] Fld = typeof(T).GetFields();
            StringBuilder column = new StringBuilder("(");
            StringBuilder values = new StringBuilder("(");

            for (int i = 0; i < Fld.Length; i++)
            {
                var attrib = GetFieldNameType(Fld[i].Name);

                column.Append(attrib.FieldName);
                
                if (attrib.FieldType == "string")
                {
                    values.Append("'");
                    values.Append(name.GetType().GetField(Fld[i].Name).GetValue(name));
                    values.Append("'");
                }
                else 
                    values.Append(name.GetType().GetField(Fld[i].Name).GetValue(name));

                if (i != Fld.Length - 1)
                {
                    column.Append(", ");
                    values.Append(", ");
                }
            }
            
            column.Append(")");
            values.Append(")");
            
            string sqlIns = string.Format("Insert Into {0}{1} Values{2}", attTbl.TableName, column, values);
           
            using (SqlCeCommand SQLcmd = new SqlCeCommand(sqlIns, connect))
            {
                SqlCeTransaction tx = null;
                tx = connect.BeginTransaction();
                try
                {
                    SQLcmd.Transaction = tx;
                    SQLcmd.ExecuteNonQuery();
                    tx.Commit();
                }
                catch (SqlCeException invOp)
                {
                    myLog.Error(invOp);
                    tx.Rollback();
                    throw;
                }
            }
        }

        private TableAttribute GetTableName()
        {
            return Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute)) as TableAttribute;
        }

        private FieldAttribute GetFieldNameType(string fld)
        {
            return Attribute.GetCustomAttribute(typeof(T).GetField(fld), typeof(FieldAttribute)) as FieldAttribute;
        }
    }
}
