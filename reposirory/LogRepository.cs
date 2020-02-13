using jwt.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.reposirory
{
    public class LogRepository : ILog
    {
        public void InsertLog(string log)
        {
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Insert into Log(Log,DateLog)values(@Log,@DateLog)", cn))
                    {
                        cmd.Parameters.AddWithValue("@Log", log.Trim());
                        cmd.Parameters.AddWithValue("@DateLog",DateTime.Now);
                        cmd.ExecuteNonQuery();
                        cn.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
           
        }
    }
}
