using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwt.Data;
using jwt.Models;
using Microsoft.Data.SqlClient;

namespace jwt.reposirory
{
    public class ServicoRepository : IServico
    {
        public Servico edit(Servico model)
        {
            Servico servico = new Servico();

            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Update Servico set Id_Servico =@Id_Servico,Categoria=@Categoria,Descricao=@Descricao ", cn))
                    {
                        cmd.Parameters.AddWithValue("@Id_Servico", model.Id_Servico);
                        cmd.Parameters.AddWithValue("@Categoria", model.Categoria.IdCategoria);
                        cmd.Parameters.AddWithValue("@Descricao", model.Descricao);
                        cmd.ExecuteNonQuery();
                        servico = model;
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


        public IEnumerable<Servico> GetAllServico()
        {
            List<Servico> Lservicos = new List<Servico>();

            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select s.Id_Servico, c.Nome_Categoria, s.Descricao from Servico s inner join Categoria c  on s.Categoria = c.IdCategoria", cn))
                    {
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr.HasRows == true)
                            {
                                Lservicos.Add(
                                    new Servico()
                                    {
                                        Id_Servico = (int)dr["Id_Servico"],

                                        Categoria = new Categoria()
                                        {
                                            Nome_Categoria = dr["Nome_Categoria"].ToString()
                                        },
                                        Descricao = dr["Descricao"].ToString()
                                    }
                                );

                            }


                            else
                            {
                                Lservicos = null;
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
            return Lservicos;
        }

        public Servico GetServicoById(int Id)
        {
            Servico servico = new Servico();

            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select s.Id_Servico, c.Nome_Categoria, s.Descricao from Servico s inner join Categoria c  on s.Categoria = c.IdCategoria where  s.Id_Servico = @Id_Servico", cn))
                    {
                        dr = cmd.ExecuteReader();
                        dr.Read();
                        if (dr.HasRows == true)
                        {

                            servico = new Servico()
                            {
                                Id_Servico = (int)dr["Id_Servico"],

                                Categoria = new Categoria()
                                {
                                    Nome_Categoria = dr["Nome_Categoria"].ToString()
                                },
                                Descricao = dr["Descricao"].ToString()
                            };


                        }


                        else
                        {
                            servico = null;
                        }
                    }
                }




            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return servico;
        }

        public Servico insert(Servico model)
        {

            Servico servico = new Servico();

            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("", cn))
                    {

                        cmd.Parameters.AddWithValue("@Categoria", model.Categoria.IdCategoria);
                        cmd.Parameters.AddWithValue("@Descricao", model.Descricao);
                        cmd.ExecuteNonQuery();
                        servico = ValidateServico(model.Categoria.IdCategoria);
                    }



                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return servico;
        }

        public Servico remove(int Id)
        {
            Servico servico = new Servico();

            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Delete from Servico where Id_Servico=@Id_Servico", cn))
                    {
                        servico = GetServicoById(Id);
                        cmd.Parameters.AddWithValue("@Id_Servico", Id);
                        cn.Close();

                    }



                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return servico;
        }

        public Servico ValidateServico(int IdCategoria)
        {
            int Id = IdCategoria;
            Servico servico = null;
            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select s.Id_Servico, c.Nome_Categoria, s.Descricao from Servico s inner join Categoria c  on s.Categoria = c.IdCategoria where Categoria=@Categoria", cn))
                    {
                        cmd.Parameters.AddWithValue("@Categoria", Id);
                        dr = cmd.ExecuteReader();
                        dr.Read();
                        if (dr.HasRows == true)
                        {
                            servico = new Servico()
                            {
                                Id_Servico = (int)dr["Id_Servico"],
                                Descricao = dr["Descricao"].ToString(),
                                Categoria = new Categoria()
                                {
                                    Nome_Categoria = dr["Nome_Categoria"].ToString()
                                },

                            };


                        }

                        else
                        {
                            servico = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return servico;
        }
    }
}


