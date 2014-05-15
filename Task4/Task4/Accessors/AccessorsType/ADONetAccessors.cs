using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlServerCe;
using NLog;

namespace Accessors
{
    public class ADONetAccessors<T> : IDataAccessors<T>
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
            string qrSQL = "Select * From Student";
            var records = new List<T>();
            using (SqlCeCommand SQLcmd = new SqlCeCommand(qrSQL, connect))
            {
                SqlCeDataReader Reader = SQLcmd.ExecuteReader();
                while (Reader.Read())
                {
                    ClsStudent student = new ClsStudent();
                    student.field1 = (int)Reader["IdStud"];
                    student.field2 = Reader["FIOStud"].ToString();
                    student.field3 = (int)Reader["IdGroup"];
                    records.Add((T)Convert.ChangeType(student, typeof(T)));
                }
            }
            return records;
        }

        public T GetByName(string name)
        {
            string qrSQL = string.Format("Select * From Student Where FIOStud = '{0}'", name);
            using (SqlCeCommand SQLcmd = new SqlCeCommand(qrSQL, connect))
            {
                SqlCeDataReader Reader = SQLcmd.ExecuteReader();
                if (Reader.Read())
                {
                    ClsStudent student = new ClsStudent();
                    student.field1 = (int)Reader["IdStud"];
                    student.field2 = Reader["FIOStud"].ToString();
                    student.field3 = (int)Reader["IdGroup"];
                    return (T)Convert.ChangeType(student, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }

        public void DeleteByName(string name)
        {
            string sqlDel = string.Format("Delete From Student Where FIOStud = '{0}'", name);
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
            ClsStudent insStud = (ClsStudent)Convert.ChangeType(name, typeof(T));
            string sqlIns = string.Format("Insert Into Student(IdStud, FIOStud, IdGroup)" +
                                              "Values({0}, '{1}', {2})", insStud.field1, insStud.field2, insStud.field3);
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
    }
}
