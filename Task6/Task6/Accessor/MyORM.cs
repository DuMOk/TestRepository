using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Data.SqlServerCe;
using System.Configuration;

namespace Task6
{
    public class MyORM<T> : IDataAccessors<T>
    {
        private SqlCeConnection _connect = null;
        private ILogger _myLog = new Mylog4net();

        private void OpenConnection()
        {
            try
            {
                _connect = new SqlCeConnection();
                _connect.ConnectionString = ConfigurationManager.ConnectionStrings["DBStudent"].ConnectionString;
                _connect.Open();
            }
            catch (SqlCeException invOp)
            {
                _myLog.Log(invOp.ToString());
                throw;
            }
        }

        private void CloseConnection()
        {
            _connect.Close();
        }

        public List<T> GetAll()
        {
            OpenConnection();
            
            var attTbl = GetTableName();
            string qrSQL = string.Format("Select * From {0}", attTbl.TableName);
            var records = new List<T>();

            using (SqlCeCommand sqlCmd = new SqlCeCommand(qrSQL, _connect))
            {
                SqlCeDataReader reader = sqlCmd.ExecuteReader();
                FieldInfo[] fld = typeof(T).GetFields();
            
                while (reader.Read())
                {
                    var constructor = typeof(T).GetConstructor(new Type[0]);
                    var item = (T)constructor.Invoke(new object[0]);
                    
                    for (int i = 0; i < fld.Length; i++)
                    {
                        var attrib = GetFieldNameType(fld[i].Name);
                        item.GetType().GetField(fld[i].Name).SetValue(item, reader[attrib.FieldName]);
                    }
                    
                    records.Add(item);
                }
            }

            CloseConnection();
            return records;
        }

        public T GetByName(string name)
        {
            OpenConnection();
            
            var attTbl = GetTableName();
            FieldInfo[] fld = typeof(T).GetFields();
            string nameFld = null;
            
            for (int i = 0; i < fld.Length; i++)
            {
                var attrib = GetFieldNameType(fld[i].Name);
                if (attrib.FieldType == "string")
                    nameFld = attrib.FieldName;
            }

            string qrSQL = string.Format("Select * From {0} Where Lower({1}) = Lower('{2}')", 
                attTbl.TableName, nameFld, name);
            
            using (SqlCeCommand sqlCmd = new SqlCeCommand(qrSQL, _connect))
            {
                SqlCeDataReader reader = sqlCmd.ExecuteReader();
                
                if (reader.Read())
                {
                    var constructor = typeof(T).GetConstructor(new Type[0]);
                    var item = (T)constructor.Invoke(new object[0]);

                    for (int i = 0; i < fld.Length; i++)
                    {
                        var attrib = GetFieldNameType(fld[i].Name);
                        item.GetType().GetField(fld[i].Name).SetValue(item, reader[attrib.FieldName]);
                    }

                    CloseConnection();
                    return item;
                }
                else
                {
                    CloseConnection();
                    return default(T);
                }
            }
        }

        public void DeleteByName(string name)
        {
            OpenConnection();
            
            var attTbl = GetTableName();
            FieldInfo[] fld = typeof(T).GetFields();
            string nameFld = null;
            
            for (int i = 0; i < fld.Length; i++)
            {
                var attrib = GetFieldNameType(fld[i].Name);
               
                if (attrib.FieldType == "string")
                    nameFld = attrib.FieldName;
            }
            
            string sqlDel = string.Format("Delete From {0} Where Lower({1}) = Lower('{2}')", 
                attTbl.TableName, nameFld, name);
            
            using (SqlCeCommand sqlCmd = new SqlCeCommand(sqlDel, _connect))
            {
                SqlCeTransaction transaction = null;
                transaction = _connect.BeginTransaction();
                
                try
                {
                    sqlCmd.Transaction = transaction;
                    sqlCmd.ExecuteNonQuery();
                    transaction.Commit();
                    CloseConnection();
                }
                catch (SqlCeException invOp)
                {
                    _myLog.Log(invOp.ToString());
                    transaction.Rollback();
                    CloseConnection();
                    throw;
                }
            }
        }

        public void Insert(T name)
        {
            OpenConnection();
            
            var attTbl = GetTableName();
            FieldInfo[] fld = typeof(T).GetFields();
            StringBuilder column = new StringBuilder("(");
            StringBuilder values = new StringBuilder("(");

            for (int i = 0; i < fld.Length; i++)
            {
                var attrib = GetFieldNameType(fld[i].Name);

                column.Append(attrib.FieldName);
                
                if (attrib.FieldType == "string")
                {
                    values.Append("'");
                    values.Append(name.GetType().GetField(fld[i].Name).GetValue(name));
                    values.Append("'");
                }
                else 
                    values.Append(name.GetType().GetField(fld[i].Name).GetValue(name));

                if (i != fld.Length - 1)
                {
                    column.Append(", ");
                    values.Append(", ");
                }
            }
            
            column.Append(")");
            values.Append(")");
            
            string sqlIns = string.Format("Insert Into {0}{1} Values{2}", attTbl.TableName, column, values);
           
            using (SqlCeCommand sqlCmd = new SqlCeCommand(sqlIns, _connect))
            {
                SqlCeTransaction transaction = null;
                transaction = _connect.BeginTransaction();
                
                try
                {
                    sqlCmd.Transaction = transaction;
                    sqlCmd.ExecuteNonQuery();
                    transaction.Commit();
                    CloseConnection();
                }
                catch (SqlCeException invOp)
                {
                    _myLog.Log(invOp.ToString());
                    transaction.Rollback();
                    CloseConnection();
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
            return 
                Attribute.GetCustomAttribute(typeof(T).GetField(fld), typeof(FieldAttribute)) as FieldAttribute;
        }
    }
}
