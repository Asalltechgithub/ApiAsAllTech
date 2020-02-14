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
                    using (SqlCommand cmd = new SqlCommand("Update Servico set Categoria=@Categoria,Descricao=@Descricao where Id_Servico=@Id_Servico ", cn))
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
                        cmd.Parameters.AddWithValue("@Id_Servico", Id);
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
                    using (SqlCommand cmd = new SqlCommand("Insert into Servico (Categoria,Descricao)values(@Categoria,@Descricao)", cn))
                    {

                        cmd.Parameters.AddWithValue("@Categoria", model.Categoria.IdCategoria);
                        cmd.Parameters.AddWithValue("@Descricao", model.Descricao);
                        cmd.ExecuteNonQuery();
                        using (SqlCommand cmd2 = new SqlCommand("Select MAX(s.Id_Servico) as Id_Servico from Servico s ", cn))
                        {
                            SqlDataReader dr;
                            dr = cmd2.ExecuteReader();
                            dr.Read();
                            if (dr.HasRows == true)
                            {

                                servico = new Servico()
                                {
                                    Id_Servico = (int)dr["Id_Servico"],


                                };
                                model.Id_Servico = servico.Id_Servico;

                            }


                            else
                            {
                                servico = null;
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
            return model;
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

        public bool ValidateServico(string descricao)
        {
            
           
            SqlDataReader dr = null;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select s.Id_Servico, c.Nome_Categoria, s.Descricao from Servico s inner join Categoria c  on s.Categoria = c.IdCategoria where Descricao=@Descricao", cn))
                    {
                        cmd.Parameters.AddWithValue("@Descricao", descricao);
                        dr = cmd.ExecuteReader();
                        dr.Read();
                       if( dr.HasRows == true)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return false ;
        }
    }
}


