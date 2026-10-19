using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backPadaria.banco;
using System.Runtime.CompilerServices;

namespace backPadaria.Models;

internal class Cliente
{
    private int _id;
    private string _nome;
    private string _cpf;
    private string _telefone;
    private string _endereco;
    private int _pontos;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public string Cpf { get => _cpf; set => _cpf = value; }
    public string Telefone { get => _telefone; set => _telefone = value; }
    public string Endereco { get => _endereco; set => _endereco = value; }
    public int Pontos { get => _pontos; set => _pontos = value; }

    public Cliente(string nome, string cpf, string telefone, string endereco, int pontos)
    {

        if (nome == "") throw new Exception("O nome do cliente não pode ser vazio");
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        Endereco = endereco;
        Pontos = pontos;
    }

    public Cliente(int id, string nome, string cpf, string telefone, string endereco, int pontos)
    {
        this.Id = id;
        if (nome == "") throw new Exception("O nome do cliente não pode ser vazio");
        this.Nome = nome;
        this.Cpf = cpf;
        this.Telefone = telefone;
        this.Endereco = endereco;
        this.Pontos = pontos;
    }


    public void AdicionarPontos(int pontos)
    {
        if (pontos > 0)

        {
            this.Pontos += pontos;
        }
    }

    public static int Inserir(Cliente cliente)
    {
        // Instrução SQL: Define o comando de inserção (INSERT).
        // O uso de marcações com '@' (ex: @Nome) e uma pratica de seguranca crucial chamada de "Parametrizacao"
        // Ela impede ataques de SQL Injection, pois o banco entende o que é comando e o que é apenas texto do usuário.

        string sql = "INSERT INTO Cliente (`Nome`, `CPF`, `Telefone`, `Endereco`, `Pontos`) VALUES (@Nome, @CPF, @Telefone, @Endereco, @Pontos)";


        // Gerenciamento de recursos: Cria o objeto de comando já passando a instrução SQL.
        // O 'using' garante que os recursos de memoria ocupados pelo comando sejam liberados ao final do bloco.
        using (var cmd = new MySqlCommand(sql))
        {
            // Vinculação de Dados: Pega as propriedades do objeto C# (cliente.Nome, uuario.Email, etc.)
            // e substitui de forma segura as marcações '@' da string SQL com os valores reais.

            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
            cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);

            // Execucao: Como o comando de INSERT nao retorna tabelas (apenas executa uma ação),
            // ele envia o comando configurado para o método genérico 'executarComando' da classe 'Banco'.
            // 0 metodo retorna 1 (se o usuario for inserido com sucesso) ou -1 (se houver erro no banco).

            return Banco.ExecutarComando(cmd);
        }
    }

    public static int Deletar(Cliente cliente)
    {
        string sql = "DELETE FROM `Cliente` WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", cliente.Id);

            return Banco.ExecutarComando(cmd);
        }
    }

    public static List<Cliente> ListarTodos()
    {
        string sql = "SELECT * FROM `Cliente`";

        List<Cliente> listaClientes = new();

        using (var cmd = new MySqlCommand(sql))
        {
            // Executa a leitura. 0 bloco 'using' aqui é FUNDAMENTAL para garantir que o
            // reader seja fechado no final, o que por sua vez fecha a conexao com o banco.
            using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
            {
                // O while vai rodar uma vez para CADA linha que existir na tabela Cliente
                while (reader.Read())
                {
                    int id = reader.GetInt32("Id");
                    string nome = ValidarCampo(reader["Nome"]);
                    string cpf = ValidarCampo(reader["CPF"]);
                    string telefone = ValidarCampo(reader["Telefone"]);
                    string endereco = ValidarCampo(reader["Endereco"]);
                    int pontos = reader.GetInt32("Pontos");

                    // Cria o objeto Cliente com os dados extraídos
                    Cliente cliente = new Cliente(id, nome, cpf, telefone, endereco, pontos);

                    // Adiciona este cliente à nossa lista
                    listaClientes.Add(cliente);
                }
            }

            // Retorna a lista completa (se a tabela estiver vazia, retorna uma lista com 0 itens)
            return listaClientes;
        }
    }

    public static Cliente? ListarPorId(int id)
    {
        string sql = "SELECT * FROM `Cliente` WHERE `id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

            Cliente? cliente = null;

            while (reader.Read())
            {
                int clienteId = reader.GetInt32("Id");
                string nome = ValidarCampo(reader["Nome"]);
                string cpf = ValidarCampo(reader["CPF"]);
                string telefone = ValidarCampo(reader["Telefone"]);
                string endereco = ValidarCampo(reader["Endereco"]);
                int pontos = reader.GetInt32("Pontos");

                // Cria o objeto Cliente com os dados extraídos
                cliente = new Cliente(id, nome, cpf, telefone, endereco, pontos);
            }

            return cliente;
        }
    }

    public static int AtualizarCliente(Cliente cliente)
    {
        string sql = "UPDATE `Cliente` SET `Nome` = @Nome, `Cpf` = @CPF, `Telefone` = @Telefone, `Endereco` = @Endereco, `Pontos` = @Pontos WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            // Vinculação de Parâmetros: Protege contra SQL Injection e organiza os dados
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
            cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);

            // A chave de busca: Quem será atualizado?
            cmd.Parameters.AddWithValue("@id", cliente.Id);

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

