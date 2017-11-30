using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace MVCproject_Zoo.DAO
{
    public class DAO
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["Zoo"].ConnectionString;
        protected SqlConnection sql { get; set; }
        public void Connect()
        {
            sql = new SqlConnection(connectionString);
            sql.Open();
        }
        public void Disconnect()
        {
            sql.Close();
        }
    }
}