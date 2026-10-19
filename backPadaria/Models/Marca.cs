using backPadaria.banco;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Models;

internal class Marca
{
    private int _id;
    private string _nome;
    private string _telefone;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public string Telefone { get => _telefone; set => _telefone = value; }

    public Marca(string nome, string telefone)
    {
        this._nome = nome;
        this._telefone = telefone;
    }
    
    public Marca(int id, string nome, string telefone)
    {
        this._id = id;
        this._nome = nome;
        this._telefone = telefone;
    }

    public static int Inserir(Marca marca)
    {
        string sql = "INSERT INTO Marca (`Nome`, `Telefone`) VALUES (@Nome, @Telefone)";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", marca.Nome);
            cmd.Parameters.AddWithValue("@Telefone", marca.Telefone);
            return cmd.ExecuteNonQuery();
        }
    }

    public static int Deletar(Marca marca)
    {
        string sql = "DELETE FROM `Marca` WHERE `Id` = @id";
        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", marca.Id);
            return cmd.ExecuteNonQuery();
        }
    }

    public static List<Marca> ListarTodos()
    {
        string sql = "SELECT * FROM `Marca`";

        List<Marca> marcas = new List<Marca>();
        
        using (var cmd = new MySqlCommand(sql))
        {
            using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32("Id");
                    string nome = ValidarCampo(reader["Nome"]);
                    string telefone = ValidarCampo(reader["Telefone"]);


                    Marca marca = new Marca(id, nome, telefone);

                    marcas.Add(marca);
                }
            }
        
            return marcas;
        }
    }

    public static Marca? ListarPorId(int id)
    {
        string sql = "SELECT * FROM `Marca` WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", id);

            using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
            {
                Marca? marca = null;

                while (reader.Read())
                {
                    int marcaId = reader.GetInt32("Id");
                    string nome = ValidarCampo(reader["Nome"]);
                    string telefone = ValidarCampo(reader["Telefone"]);

                    marca = new Marca(id, nome, telefone);
                }
            
                return marca;
            }
        }
    }

    public static int AtualizarMarca(Marca marca)
    {
        string sql = "UPDATE `Marca` SET `Nome` = @Nome, `Telefone` = @Telefone WHERE `Id` = @id";
        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", marca.Id);
            cmd.Parameters.AddWithValue("@Nome", marca.Nome);
            cmd.Parameters.AddWithValue("@Telefone", marca.Telefone);
            return cmd.ExecuteNonQuery();
        }
    }

    private static string ValidarCampo(object campo)
    {
        if (campo == DBNull.Value || campo == null)
        {
            return "";
        }
        else
        {
            return campo.ToString();
        }
    }
}
