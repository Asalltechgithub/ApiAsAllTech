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
            string query = "Update Portifolio set Categoria=@Categoria,Titulo=@Titulo,Imagem=@Imagem,Link=@Link where Id_Portifolio=@Id_Portifolio ";

            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@Id_Portifolio", model.Id_Portifolio);
                        cmd.Parameters.AddWithValue("@Catetgoria", model.categoria);
                        cmd.Parameters.AddWithValue("@Titulo", model.Titulo);
                        cmd.Parameters.AddWithValue("@Imagem", model.Imagem);
                        cmd.Parameters.AddWithValue("@Link", model.Link);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return model;
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
            string query = "Select * from Portifolio where Id_Portifolio = @Id_Portifolio";

            SqlDataReader dr;
            Portifolio p = new Portifolio();
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows == false)
                        {
                            return null;
                        }
                        else
                        {
                            p = new Portifolio
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
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return p;
        }

        public Portifolio Insert(Portifolio model)
        {
            string query = "Insert into Portifolio values(@Categoria,@Titulo,@Imagem,@link)";
            try
            {
                if (ValidatePortifolio(model.Titulo) == false)
                    using (SqlConnection cn = Conexao.conectar())
                    {
                        using (SqlCommand cmd = new SqlCommand(query, cn))
                        {
                            cmd.Parameters.AddWithValue("@Catetgoria", model.categoria);
                            cmd.Parameters.AddWithValue("@Titulo", model.Titulo);
                            cmd.Parameters.AddWithValue("@Imagem", model.Imagem);
                            cmd.Parameters.AddWithValue("@Link", model.Link);
                            cmd.ExecuteNonQuery();
                        }
                    }
                else
                    model = null;
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return model;
        }

        public Portifolio remove(int Id)
        {
            string query = "Delete from Portifolio where Id_Portifolio = @Id_Portifolio";
            var model = GetPortifolioById(Id);
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        if (model == null)
                        {
                            return null;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Id_Portifolio", Id);
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return model;
        }
        public bool ValidatePortifolio(string name)
        {
            bool state = false;
            string query = "Select * from Portifolio where Id_Portifolio = @Id_Portifolio";

            SqlDataReader dr;
            Portifolio p = new Portifolio();
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows == false)
                        {
                            return state = false;
                        }
                        else
                        {

                            return state = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return state;

        }
    }
}
