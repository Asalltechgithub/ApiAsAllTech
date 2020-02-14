using jwt.Data;
using jwt.Models;
using jwt.services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.reposirory
{
    public class UsuarioRepository : IUsuario
    {




        public User Edit(User model)
        {
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Update Usuario set Username=@Username,Role=@Role where Id = @Id", cn))
                    {
                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.Parameters.AddWithValue("@Username", model.Username);
                        cmd.Parameters.AddWithValue("@Role", model.Role);
                        cmd.ExecuteNonQuery();
                        cn.Close();

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

        public User GetUser(string Username, string password)
        {
            User usuario = new User();
            bool ckSenha = false;
            Encryption encryption = new Encryption();
            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from Usuario u inner join GrupoUsuario gu on u.Role = gu.IdGrupoUsuario where Username = @Username", cn))
                    {

                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        dr = cmd.ExecuteReader();

                        dr.Read();
                        if (dr.HasRows == true)
                        {

                            usuario.Id = (int)(dr["Id"]);
                            usuario.Username = dr["Username"].ToString();
                            usuario.Role = dr["Role"].ToString();
                            usuario.Password = dr["Password"].ToString();
                            usuario.grupo = new GrupoUsuario()
                            {
                                NomeGrupoUsuario = dr["NomegrupoUsuario"].ToString()
                            };

                        } else if (dr.HasRows ==false){
                           return usuario = null;

                        }
                       

                        // metodo  que compara senha ecriptada 
                        ckSenha = encryption.checkEncryptPassword(usuario.Password, password);
                        usuario.Password = string.Empty;
                        if (ckSenha == false)
                        {
                            return null;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }

            return usuario;
        }

        public IEnumerable<User> GetUsersByRole(int Role)
        {
            User usuario = new User();
            List<User> List = new List<User>();

            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from Usuario u inner join GrupoUsuario gu on u.Role = gu.IdGrupoUsuario  where u.Role=@Role", cn))
                    {
                        cmd.Parameters.AddWithValue("@Role", Role);
                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {

                            if (dr.HasRows == true)
                            {
                                List.Add(
                                    new User()
                                    {
                                        Id = (int)dr["Id"],
                                        Username = dr["Username"].ToString(),
                                        Role = dr["Role"].ToString(),
                                        grupo = new GrupoUsuario()
                                        {
                                            IdGrupoUsuario =(int) dr["IdGrupoUsuario"],
                                           NomeGrupoUsuario =  dr["NomeGrupoUsuario"].ToString()
                                        }


                                    }

                                    ) ;
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

            return List;
        }
        public IEnumerable<User> GetAllUsers()
        {
            User usuario = new User();
            List<User> List = new List<User>();

            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select u.Id, u.Role, u.Username ,gu.NomeGrupoUsuario from Usuario u inner join GrupoUsuario gu on u.Role = gu.IdGrupoUsuario ", cn))
                    {
                        
                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {

                            if (dr.HasRows == true)
                            {
                                List.Add(
                                    new User()
                                    {
                                        Id = (int)dr["Id"],
                                        Username = dr["Username"].ToString(),
                                        Role = dr["Role"].ToString(),
                                        grupo = new GrupoUsuario()
                                        {
                                            NomeGrupoUsuario = dr["NomeGrupoUsuario"].ToString()
                                        }
                                    }


                                ) ;
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

            return List;
        }
        public User Insert(User model)
        {

            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("insert into Usuario(Username,Password,Role)values(@Username,@Password,@Role)", cn))
                    {
                        cmd.Parameters.AddWithValue("@Username", model.Username);
                        cmd.Parameters.AddWithValue("@Password", model.Password);
                        cmd.Parameters.AddWithValue("@Role", model.Role);
                        cmd.ExecuteNonQuery();
                        cn.Close();
                        model = ValidUserName(model.Username);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return model;
        }

        public User Remove(User model)
        {
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Delete from Usuario where Id =@Id", cn))
                    {
                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.ExecuteNonQuery();
                        cn.Close();

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

        public User GetUserById(int Id)
        {
            User usuario = new User();


            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select u.Id, u.Role, u.Username ,gu.NomeGrupoUsuario from Usuario u inner join GrupoUsuario gu on u.Role = gu.IdGrupoUsuario where u.Id = @Id", cn))
                    {
                        cmd.Parameters.AddWithValue("@Id", Id);
                        dr = cmd.ExecuteReader();

                        dr.Read();


                        if (dr.HasRows == true)
                        {

                            usuario.Id = (int)dr["Id"];
                            usuario.Username = dr["Username"].ToString();
                            usuario.Role = dr["Role"].ToString();
                            usuario.grupo.NomeGrupoUsuario = dr["NomeGrupoUsuario"].ToString();
                        }

                    }



                }



            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }

            return usuario;
        }

        public User ValidUserName(string UserName)
        {
            User usuario = new User();
            bool Status = false;
            Encryption encryption = new Encryption();
            SqlDataReader dr;
            try
            {
                using (SqlConnection cn = Conexao.conectar())
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from Usuario u inner join GrupoUsuario gu on u.Role = gu.IdGrupoUsuario where u.Username= @Username", cn))
                    {

                        cmd.Parameters.AddWithValue("@Username", UserName);


                        dr = cmd.ExecuteReader();

                        dr.Read();
                        if (dr.HasRows == true)
                        {
                            usuario.Username = dr["Username"].ToString();
                            usuario.Id = (int)dr["Id"];
                            usuario.Role = dr["Role"].ToString();
                            usuario.grupo.NomeGrupoUsuario = dr["NomeGrupoUsuario"].ToString();
                            return usuario;
                        }

                        else
                        {
                            return usuario = null;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                LogRepository log = new LogRepository();
                log.InsertLog(ex.Message);
            }

            return usuario;

        }
    }
}
