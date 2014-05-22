using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlServerCe;
using System.Configuration;

namespace Task6
{
    public class ADONetAccessors<T> : IDataAccessors<T>
    {
        private SqlCeConnection _connect = null;
        private ILogger _myLog = new MyNLog();

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

            string qrSQL = "Select * From Student";
            var records = new List<T>();
            
            using (SqlCeCommand sqlCmd = new SqlCeCommand(qrSQL, _connect))
            {
                SqlCeDataReader reader = sqlCmd.ExecuteReader();
 
                while (reader.Read())
                {
                    Student newStudent = new Student();
                    
                    newStudent.field1 = (int)reader["IdStud"];
                    newStudent.field2 = reader["FIOStud"].ToString();
                    newStudent.field3 = (int)reader["IdGroup"];
                    
                    records.Add((T)Convert.ChangeType(newStudent, typeof(T)));
                }
            }

            CloseConnection();
            return records;
        }

        public T GetByName(string name)
        {
            OpenConnection();
            
            string qrSQL = string.Format("Select * From Student Where FIOStud = '{0}'", name);
            
            using (SqlCeCommand sqlCmd = new SqlCeCommand(qrSQL, _connect))
            {
                SqlCeDataReader reader = sqlCmd.ExecuteReader();
                
                if (reader.Read())
                {
                    Student newStudent = new Student();
                    
                    newStudent.field1 = (int)reader["IdStud"];
                    newStudent.field2 = reader["FIOStud"].ToString();
                    newStudent.field3 = (int)reader["IdGroup"];

                    CloseConnection();
                    return (T)Convert.ChangeType(newStudent, typeof(T));
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
            
            string sqlDel = string.Format("Delete From Student Where FIOStud = '{0}'", name);
            
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
            
            Student insStud = (Student)Convert.ChangeType(name, typeof(T));
            string sqlIns = string.Format("Insert Into Student(IdStud, FIOStud, IdGroup)" +
                                              "Values({0}, '{1}', {2})", insStud.field1, insStud.field2, insStud.field3);
            
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
    }
}
