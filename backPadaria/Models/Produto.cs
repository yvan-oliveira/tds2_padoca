using backPadaria.banco;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Models;

internal class Produto
{
    private int _id;
    private string _nome;
    private decimal _preco;
    private string _categoria;
    private bool _fabricado;
    private int _marcaId;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public decimal Preco { get => _preco; set => _preco = value; }
    public string Categoria { get => _categoria; set => _categoria = value; }
    public bool Fabricado { get => _fabricado; set => _fabricado = value; }
    public int MarcaId { get => _marcaId; set => _marcaId = value; }

    public Produto(string nome, decimal preco, string categoria, bool fabricado, int marcaId)
    {
        this._nome = nome;
        this._preco = preco;
        this._categoria = categoria;
        this._fabricado = fabricado;
        this._marcaId = marcaId;
    }

    public Produto(int id, string nome, decimal preco, string categoria, bool fabricado, int marcaId)
    {
        this._id = id;
        this._nome = nome;
        this._preco = preco;
        this._categoria = categoria;
        this._fabricado = fabricado;
        this._marcaId = marcaId;
    }

    public static int Inserir(Produto produto)
    {
        string sql = "INSERT INTO Produto (`Nome`, `Preco`, `Categoria`, `Fabricado`, `MarcaId`) VALUES (@Nome, @Preco, @Categoria, @Fabricado, @MarcaId)";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", produto.Nome);
            cmd.Parameters.AddWithValue("@Preco", produto.Preco);
            cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
            cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
            cmd.Parameters.AddWithValue("@MarcaId", produto.MarcaId);
            return cmd.ExecuteNonQuery();
        }
    }

    public static int Deletar(Produto produto)
    {
        string sql = "DELETE FROM `Produto` WHERE `Id` = @id";
        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", produto.Id);
            return cmd.ExecuteNonQuery();
        }
    }

    public static List<Produto> ListarTodos()
    {
        string sql = "SELECT * FROM `Produto`";

        List<Produto> produtos = new List<Produto>();

        using (var cmd = new MySqlCommand(sql))
        {
            using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32("Id");
                    string nome = ValidarCampo(reader["Nome"]);
                    decimal preco = reader.GetDecimal("Preco");
                    string categoria = ValidarCampo(reader["Categoria"]);
                    bool fabricado = reader.GetBoolean("Fabricado");
                    int marcaId = reader.GetInt32("MarcaId");

                    Produto produto = new Produto(nome, preco, categoria, fabricado, marcaId);

                    produtos.Add(produto);
                }
            }

            return produtos;
        }
    }

    public static Produto? ListarPorId(int id)
    {
        string sql = "SELECT * FROM `Produto` WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", id);

            using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
            {
                Produto? produto = null;

                while (reader.Read())
                {
                    int idProduto = reader.GetInt32("Id");
                    string nome = ValidarCampo(reader["Nome"]);
                    decimal preco = reader.GetDecimal("Preco");
                    string categoria = ValidarCampo(reader["Categoria"]);
                    bool fabricado = reader.GetBoolean("Fabricado");
                    int marcaId = reader.GetInt32("MarcaId");

                    produto = new Produto(id, nome, preco, categoria, fabricado, marcaId);
                }

                return produto;
            }
        }
    }

    public static int AtualizarMarca(Produto produto)
    {
        string sql = "UPDATE `Produto` SET `Nome` = @Nome, `Preco` = @Preco, `Categoria` = @Categoria, `Fabricado` = @Fabricado, `MarcaId` = @Marca WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", produto.Nome);
            cmd.Parameters.AddWithValue("@Preco", produto.Preco);
            cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
            cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
            cmd.Parameters.AddWithValue("@MarcaId", produto.MarcaId);
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
