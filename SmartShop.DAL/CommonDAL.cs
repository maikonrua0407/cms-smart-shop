using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartShop.DAL
{
    public class CommonDAL
    {
        private IDbConnection conn;
        private IDbDataAdapter da;
        private IDbCommand cmd;

        public DataSet ExecuteQuery(string SQL)
        {
            DataSet ds = new DataSet();
            String strConnString = Utility.ConnectionString;
            SqlCommand cmd = new SqlCommand(SQL);
            SqlConnection con = new SqlConnection(strConnString);
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(ds);
            }
            catch
            {
                return ds;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                sda.Dispose();
            }
            return ds;
        }
    }
}