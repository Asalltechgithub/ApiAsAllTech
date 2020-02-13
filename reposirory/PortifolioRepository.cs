using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwt.Data;
using jwt.Models;
using Microsoft.Data.SqlClient;

namespace jwt.reposirory
{
    public class PortifolioRepository : IPortifolio
    {
        public Portifolio Edit(Portifolio model)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (SqlConnection cn = Conexao.conectar())
            //    {
            //        using (SqlCommand cmd = new SqlCommand("", cn))
            //        {

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogRepository log = new LogRepository();
            //    log.InsertLog(ex.Message);
            //}
        }

        public IEnumerable<Portifolio> GetAllPortifolio()
        {
            string query = "Select *from Portifolio p inner join  Categoria c on p.Categoria = c.IdCategoria";
            SqlDataReader dr;
            List<Portifolio> list = new List<Portifolio>();
            Portifolio p = new Portifolio();
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr.HasRows == null)
                            {
                                return null;
                            }
                            else
                            {
                                list.Add(

                                   p = new Portifolio()
                                   {
                                       Id_Portifolio = Convert.ToInt32(dr["Id_Portifolio"]),
                                       Titulo = dr["Titulo"].ToString(),
                                       categoria = new Categoria
                                       {
                                           IdCategoria = Convert.ToInt32(dr["Categoria"]),
                                           Nome_Categoria = dr["Nome_Categoria"].ToString()

                                       },
                                       Imagem = dr["Imagem"].ToString(),
                                       Link = dr["Link"].ToString()
                                   });
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return list;
        }

        public Portifolio GetPortifolioById(int Id)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (SqlConnection cn = Conexao.conectar())
            //    {
            //        using (SqlCommand cmd = new SqlCommand("", cn))
            //        {

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogRepository log = new LogRepository();
            //    log.InsertLog(ex.Message);
            //}
        }

        public Portifolio Insert(Portifolio model)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (SqlConnection cn = Conexao.conectar())
            //    {
            //        using (SqlCommand cmd = new SqlCommand("", cn))
            //        {

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogRepository log = new LogRepository();
            //    log.InsertLog(ex.Message);
            //}
        }

        public Portifolio remove(int Id)
        {
            throw new  NotImplementedException(); 
        //    try
        //    {
        //        using (SqlConnection cn = Conexao.conectar())
        //        {
        //            using (SqlCommand cmd = new SqlCommand("", cn))
        //            {

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogRepository log = new LogRepository();
        //        log.InsertLog(ex.Message);
        //    }
        }
    }
}
