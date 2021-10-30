using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CursoWindowsFormsBiblioteca.Databases
{
    public class SQLServerClass
    {
        public string stringConn;
        public SqlConnection connDB;

        public SQLServerClass()
        {
            try
            {
                //criando a conexão
                //stringConn = "Data Source=DESKTOP-S1FUTDS;Initial Catalog=ByteBank;Persist Security Info=True;User ID=sa;Password=holanda1";
                stringConn = ConfigurationManager.ConnectionStrings["Fichario"].ConnectionString; // => app.config
                connDB = new SqlConnection(stringConn);
                // abrir conexao
                connDB.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SQLQuery(string SQL) // tabela em memória
        {
            DataTable dt = new DataTable();
            try
            {
                var myCommand = new SqlCommand(SQL, connDB); // pacote para ser enviado para o banco
                myCommand.CommandTimeout = 0; // não tem timeout => espera o tempo que for necessário 
                var myReader = myCommand.ExecuteReader(); //possui os dados
                dt.Load(myReader); // dt vai ter o conteúdo do retorno do comando sql
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //comandos p estrutura de dados update/insert/select ...
        public string SQLCommand(string SQL)
        {
            try
            {
                var myCommand = new SqlCommand(SQL, connDB); // pacote para ser enviado para o banco
                myCommand.CommandTimeout = 0; // não tem timeout => espera o tempo que for necessário 
                var myReader = myCommand.ExecuteReader();
                return ""; // se n houver erro retorna vazio 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // fechar a conexao
        public void Close()
        {
            connDB.Close();
        }
    }
}
