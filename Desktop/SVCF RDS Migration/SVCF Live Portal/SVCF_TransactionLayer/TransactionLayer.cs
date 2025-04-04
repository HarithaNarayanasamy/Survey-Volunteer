using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SVCF_DataAccessLayer;
namespace SVCF_TransactionLayer
{
    public class TransactionLayer
    {
        Transcation objtran;

        public TransactionLayer()
        {
            objtran = new Transcation();
        }

        public void DisposeTrn()
        {
            objtran.Dispose();
        }

        public void CommitTrn()
        {
            objtran.Commit();
        }

        public void RollbackTrn()
        {
            objtran.Rollback();
        }

        public long AddRow(string database, string table, string[] columns, object[] values, string binary_column = null, byte[] binary_data = null, string updateWhere = null)
        {
            return objtran.AddRow(database, table, columns, values, binary_column, binary_data, updateWhere);
        }

        //public long AddRow(string database, string table, List<ColumnAndData> listColData, string updateWhere = null)
        //{
        //    return objtran.AddRow(database, table, listColData, updateWhere);
        //}

        public long insertorupdateTrn(string strQurey)
        {
            return objtran.insertorupdate(strQurey);
        }

        public void SendQueryTrn(string query)
        {
            objtran.SendQuery(query);
        }

        public object GetObjectTrn(string query)
        {
            return objtran.GetObject(query);
        }

        public int GetIntTrn(string query)
        {
            return objtran.GetInt(query);
        }

        public uint GetUintTrn(string query)
        {
            return objtran.GetUint(query);
        }

        public string GetStringTrn(string query)
        {
            return objtran.GetString(query);
        }

        public DataTable GetTableTrn(string query)
        {
            return objtran.GetTable(query);
        }

        public void BulkSendTrn(string database, string table, string column, List<object> listData)
        {
            objtran.BulkSend(database, table, column, listData);
        }

        public void BulkSendTrn(string database, string table, DataTable dataTable)
        {
            objtran.BulkSend(database, table, dataTable);
        }
    }
}
