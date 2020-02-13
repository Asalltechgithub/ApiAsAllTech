using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.Data
{
    public static class Conexao
    {

       public static SqlConnection conectar()
        {
            string conStr = @"Data Source=DESKTOP-2A4F5RD\SQLEXPRESS;Initial Catalog=dbSite;User ID=sa;Password=230600";
            SqlConnection cn = new SqlConnection();

            cn.ConnectionString = conStr;
            cn.Open();
            return cn;
        }


    }
}
