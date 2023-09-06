﻿using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inlock.webApi.Repository
{

    public class UsuarioRepository : IUsuarioRepository
    {
        private string StringConexao = "Data Source = NOTE18-S14; Initial Catalog = Filmes_Tarde; User Id = sa; Pwd = Senai@134";

        public UsuarioDomain Logar(string Email, string Senha)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryUsuario = "SELECT IdUsuario, IdTipoUsuario, Email, Senha FROM Usuario WHERE Email = @Email AND Senha = @Senha";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryUsuario, con))
                {
                    cmd.Parameters.AddWithValue("@Email", Email);

                    cmd.Parameters.AddWithValue("@Senha", Senha);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuarioBuscado = new UsuarioDomain()
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),

                            Email = rdr["Email"].ToString(),

                            Senha = rdr["Senha"].ToString(),


                        };
                        return usuarioBuscado;
                    }
                    return null;
                }

            }
        }
    }
}

