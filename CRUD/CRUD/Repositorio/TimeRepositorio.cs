using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.Repositorio
{
    public class TimeRepositorio
    {
        private SqlConnection _con;

        private void Connection() //classe privada só pode ser usada na classe do proprio escopo
        {
            string constr = ConfigurationManager.ConnectionStrings["stringConexao"].ToString(); //stringconexao tá na web config
            _con = new SqlConnection(constr); //variavel de conexao 
        }

        //Adicionar Time


        public bool AdicionarTime(Times timeObj) //criou um objeto refereciando a 
        {
            int i;
            Connection();
            using (SqlCommand conexao = new SqlCommand("InserirTime", _con))
            {
                conexao.CommandType = CommandType.StoredProcedure;
                _con.Open();
                conexao.Parameters.AddWithValue("@nome", timeObj.nm_time); //adicionando valor para cada parametro da procedure
                conexao.Parameters.AddWithValue("@sigla", timeObj.sg_estado);
                conexao.Parameters.AddWithValue("@cores", timeObj.nm_cores);
                i = conexao.ExecuteNonQuery(); //adicionou 
            }
            _con.Close();
            return i >= 1;
        }

        //obter todos os times
        public List<Times> MostraTimes() //metodo public para listar os times
        {
            Connection(); //chamou a conexao
            List<Times> timesList = new List<Times>(); //atribuiu a listagem para uma variavel 
            using (SqlCommand command = new SqlCommand("MostrarTimes", _con)) //iniciou as consultas para procedure
            {
                command.CommandType = CommandType.StoredProcedure; //disse que vai a consulta vai ser com procedure
                _con.Open(); //abriu a conexao   
                SqlDataReader reader = command.ExecuteReader(); //criou uma variavel procedura para mandar poder mostrar o retorno da procedure

                while (reader.Read()) //mostrar na tela todos os dados.
                {
                    Times times = new Times() //fez isso para poder chamar da classe times e aí, instaciou um novo objeto
                    {
                        cd_time = Convert.ToInt32(reader["cd_time"]),//dados da classe "primária"
                        nm_time = Convert.ToString(reader["nm_time"]),
                        nm_cores = Convert.ToString(reader["nm_cores"]),
                        sg_estado = Convert.ToString(reader["sg_estado"])
                    };
                    timesList.Add(times); //tá adicionando o objeto chamaTime(l53) no timesList(l44)
                }
                _con.Close();
                return timesList;
            }
        }

        

        //Adicionar Time
        public bool AlterarTime(Times timeObj) //criei um método booleano para Alterar os Times(UPDATE) e instanciei um objeto (timeobj) para ter acesso a classe times
        {
            int i;
            Connection(); //disse q ia conectar
            using(SqlCommand conexao = new SqlCommand("AlterarTime", _con)) //criei um comando sql para para poder usar uma query do banco.
            {
                conexao.CommandType = CommandType.StoredProcedure; //avisei ao sistema que vou trabalhar com stored procedure
                _con.Open();
                conexao.Parameters.AddWithValue("@codigo", timeObj.cd_time);
                conexao.Parameters.AddWithValue("@nome", timeObj.nm_time);
                conexao.Parameters.AddWithValue("@sigla", timeObj.sg_estado);
                conexao.Parameters.AddWithValue("@cores", timeObj.nm_cores);
                i = conexao.ExecuteNonQuery();
            };
            _con.Close();
            return i >= 1;
        }

        public bool ExcluirTime(int id)
        {
            int i;
            Connection();
            using(SqlCommand consulta = new SqlCommand("ExcluirTime", _con))
            {
                consulta.CommandType = CommandType.StoredProcedure;
                _con.Open();
                consulta.Parameters.AddWithValue("@codigo", id);
                i = consulta.ExecuteNonQuery();
            };
            _con.Close();
            if(i >= 1)
            {
                return true;
            }
            return false;
        }
   
    }
}