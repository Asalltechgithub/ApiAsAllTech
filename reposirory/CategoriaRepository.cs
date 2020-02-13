using jwt.Data;
using jwt.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.reposirory
{
    public class CategoriaRepository : ICategoria
    {
        public IEnumerable<Categoria> GetAllCategorias()
        {
            List<Categoria> Lcategorias = new List<Categoria>();

            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from Categoria", cn))
                    {
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr.HasRows == true)
                            {
                                Lcategorias.Add(
                              new Categoria
                              {
                                  IdCategoria = (int)dr["IdCategoria"],
                                  Nome_Categoria = dr["Nome_Categoria"].ToString()
                              }

                              );
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return Lcategorias;
        }

        public Categoria GetCategoriaById(int Id)
        {
            
            Categoria cat = new Categoria();
            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from Categoria where IdCategoria = @IdCategoria", cn))
                    {
                        cmd.Parameters.AddWithValue("IdCategoria", Id);
                        dr = cmd.ExecuteReader();
                        dr.Read();

                        if (dr.HasRows == true)
                        {

                            cat = new Categoria
                            {
                                IdCategoria = (int)dr["IdCategoria"],
                                Nome_Categoria = dr["Nome_Categoria"].ToString()
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
            return cat;
        }
    

    public Categoria insert(Categoria model)
    {

            try
            {
                using(SqlConnection cn = Conexao.conectar())
                {
                    using(SqlCommand cmd = new SqlCommand("Insert into Categoria(Nome_Categoria)values(@Nome_Categoria)", cn))
                    {
                        cmd.Parameters.AddWithValue("@Nome_Categoria",model.Nome_Categoria);
                        cmd.ExecuteNonQuery();
                        cn.Close();
                        model = ValidCategoria(model.Nome_Categoria);
                    }
                }
            }
            catch(Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return model;
    }

    public Categoria remove(int Id)
    {
            var Cat = new Categoria();
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("delete from Categoria where IdCategoria=@IdCategoria", cn))
                    {
                        Cat = GetCategoriaById(Id);
                        cmd.Parameters.AddWithValue("@IdCategoria", Id);
                        cmd.ExecuteNonQuery();
                        cn.Close();
                        
                    }
                }
            }
            catch(Exception ex)
            {
                Cat = null;
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
                return null;
            }
            return Cat;
    }

    public Categoria update(Categoria model)
    {
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Update Categoria set Nome_Categoria=@Nome_Categoria where IdCategoria=@IdCategoria", cn))
                    {
                        cmd.Parameters.AddWithValue("@IdCategoria", model.IdCategoria);
                        cmd.Parameters.AddWithValue("@Nome_Categoria", model.Nome_Categoria);
                        cmd.ExecuteNonQuery();
                        cn.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
                return null;
            }
            return model;

        }

        public Categoria ValidCategoria(string NomeCategoria)
        {
            Categoria cat = new Categoria();
           
            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from Categoria where Nome_Categoria = @Nome_Categoria", cn))
                    {
                        cmd.Parameters.AddWithValue("Nome_Categoria", NomeCategoria);
                        dr = cmd.ExecuteReader();
                        dr.Read();

                        if (dr.HasRows == true)
                        {

                            cat = new Categoria
                            {
                                IdCategoria = (int)dr["IdCategoria"],
                                Nome_Categoria = dr["Nome_Categoria"].ToString()
                            };

                            return cat;

                        }
                        else
                        {
                            cat = null;
                        }
                       



                    }
                }

            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }
            return cat;
        }
    }
}
