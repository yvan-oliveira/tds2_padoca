using backPadaria.banco;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Models;

internal class Fornecedor
{
    private int _id;

    private string _nome;

    private string _telefone;

    private string _cnpj;


    public int Id { get => _id; set => _id = value; }

    public string Nome { get => _nome; set => _nome = value; }

    public string Telefone { get => _telefone; set => _telefone = value; }

    public string CNPJ { get => _cnpj; set => _cnpj = value; }

    public Fornecedor(int id, string nome, string cnpj, string telefone)

    {

        this._id = id;

        this._nome = nome;

        this._cnpj = cnpj;

        this._telefone = telefone;

    }

    public Fornecedor(string nome, string cnpj, string telefone)

    {

        this._nome = nome;

        this._cnpj = cnpj;

        this.Telefone = telefone;

    }

    public int Inserir(Fornecedor fornecedor)

    {

        string sql = "INSERT INTO Fornecedor (`Nome`, `CNPJ`, `Telefone`) VALUES (@Nome, @CNPJ, @Telefone)";

        using (var cmd = new MySqlCommand(sql))

        {

            // Vinculação de Dados: Pega as propriedades do objeto C#

            // e substitui de forma segura as marcações `@` da string SQL com os valores reais.

            cmd.Parameters.AddWithValue("@Nome", fornecedor.Nome);

            cmd.Parameters.AddWithValue("@CNPJ", fornecedor.CNPJ);

            cmd.Parameters.AddWithValue("@Telefone", fornecedor.Telefone);

            // Execucao: Como o comando de INSERT nao retorna tabelas (apenas executa uma ação),

            // ele envia o comando configurado para o método genérico `executarComando` da classe `Banco`.

            // 0 metodo retorna 1 (se a marca for inserida com sucesso) ou -1 (se houver erro no banco).

            return Banco.ExecutarComando(cmd);

        }

    }

    public static int Deletar(Fornecedor f)

    {

        string sql = "DELETE FROM `Fornecedor` WHERE `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))

        {

            cmd.Parameters.AddWithValue("id", f.Id);

            return Banco.ExecutarComando(cmd);

        }

    }

    public static List<Fornecedor> ListarFornecedores()

    {

        string cmdSql = "SELECT * FROM `Fornecedor`";

        List<Fornecedor> listaFornecedores = new();

        using (var cmd = new MySqlCommand(cmdSql))

        {

            // Executa a leitura. 0 bloco `using` aqui é FUNDAMENTAL para garantir que o

            // reader seja fechado no final, o que por sua vez fecha a conexao com o banco.

            using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))

            {

                // O while vai rodar uma vez para CADA linha que existir na tabela Cliente

                while (reader.Read())

                {

                    // Extrai os dados da linha atual

                    int id = reader.GetInt32("Id");

                    string nome = ValidarCampo(reader["Nome"]);

                    string cnpj = ValidarCampo(reader["CNPJ"]);

                    string telefone = ValidarCampo(reader["Telefone"]);

                    // Cria o objeto Cliente com os dados extraidos

                    Fornecedor fornecedor = new(id, nome, cnpj, telefone);

                    // Adiciona este cliente a nossa lista

                    listaFornecedores.Add(fornecedor);

                }

            }

            return listaFornecedores;

        }

    }

    public static Fornecedor? ListarPorId(int id)

    {

        string sql = "SELECT * FROM `Fornecedor` WHERE `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))

        {

            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

            Fornecedor fornecedor = null;

            while (reader.Read())

            {

                // Extrai os dados da linha atual

                int idBanco = reader.GetInt32("Id");

                string nome = ValidarCampo(reader["Nome"]);

                string cnpj = ValidarCampo(reader["CNPJ"]);

                string telefone = ValidarCampo(reader["Telefone"]);

                // Cria o objeto Cliente com os dados extraidos

                fornecedor = new(id, nome, cnpj, telefone);

            }

            return fornecedor;

        }

    }

    public static int AtualizarMarca(Fornecedor fornecedor)

    {

        string sql = "UPDATE `Fornecedor` SET `Nome` = @Nome, `CNPJ` = @CNPJ, `Telefone` = @Telefone WHERE `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))

        {

            cmd.Parameters.AddWithValue("@Nome", fornecedor.Nome);

            cmd.Parameters.AddWithValue("@CNPJ", fornecedor.CNPJ);

            cmd.Parameters.AddWithValue("@Telefone", fornecedor.Telefone);

            // A chave de busca: Quem será atualizado?

            cmd.Parameters.AddWithValue("@Id", fornecedor.Id);

            // Execução: O UPDATE também é uma instrução de ação (como INSERT e DELETE).

            // Ele vai retornar 1 (se encontrou  o ID e atualizou com sucesso) ou 0 (se o ID não existia).

            return Banco.ExecutarComando(cmd);

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
