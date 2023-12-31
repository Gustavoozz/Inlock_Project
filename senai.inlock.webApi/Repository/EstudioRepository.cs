﻿using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inlock.webApi.Repository
{
    public class EstudioRepository : IEstudioRepository
    {
        private string StringConexao = "Data Source = NOTE18-S14; Initial Catalog = inlock_games_tarde; User Id = sa; Pwd = Senai@134";

        /// <summary>
        /// Método responsável por cadastrar um novo estúdio.
        /// </summary>
        /// <param name="novoEstudio"></param>
        public void Cadastrar(EstudioDomain novoEstudio)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query que será executada.
                string queryInsert = $"INSERT INTO Estudio VALUES(@Nome)";

                // Declara o SqlCommand passando a query que será executada e a conexão com o BD.
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    
                    cmd.Parameters.AddWithValue("@Nome", novoEstudio.Nome);


                    // Abre a conexão com o banco de dados.
                    con.Open();

                    // Executa a query.
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Método responsável por listar os estúdios já cadastrados.
        /// </summary>
        /// <returns></returns>
        public List<EstudioDomain> ListarTodos()
        {
                //Cria uma lista de gêneros para armazená-los
                List<EstudioDomain> ListaEstudios = new();

                //Declara a SqlConnection passando a String de Conexão como parâmetro
                using (SqlConnection con = new(StringConexao))
                {
                    //Declara a instrução a ser executada
                    string querySelectAll = "SELECT IdEstudio, Nome FROM Estudio";

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Declara o SqlDataReader para percorrer (ler) a tabela no banco de dados
                    SqlDataReader rdr;

                    //Declara o SqlCommand passando a query que será executada e a conexão
                    using SqlCommand cmd = new(querySelectAll, con);
                    //Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    //Enquanto houver registros para serem lidos no rdr, o laço se repetirá.
                    while (rdr.Read())
                    {
                        EstudioDomain Estudio = new()
                        {
                            //Atribui à propriedade IdFilme os valores das colunas
                            IdEstudio = Convert.ToInt32(rdr["IdEstudio"]),       
                            
                            Nome = rdr["Nome"].ToString(),
                        };

                        ListaEstudios.Add(Estudio);
                    };
                }

                //Retorna a lista de gêneros
                return ListaEstudios;


            }
        }
    }

