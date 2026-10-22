using backPadaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service
{
    internal class MarcaService
    {
        public static void MenuMarca()
        {
            bool sair = false;
            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("===== GESTÃO DE MARCA ===== ");
                Console.WriteLine("1 - Cadastrar Marca");
                Console.WriteLine("2 - Listar Todos os Marca");
                Console.WriteLine("3 - Buscar Marca por ID");
                Console.WriteLine("4 - Atualizar Marca");
                Console.WriteLine("5 - Excluir Marca");
                Console.WriteLine("0 - Sair do Programa");

                Console.WriteLine(" =============================");
                Console.Write("Digite uma opção: ");

                string opcao = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            CadastrarMarca(); break;
                        case "2":
                            ListarMarca(); break;
                        case "3":
                            BuscarMarcaPorId(); break;
                        case "4":
                            AtualizarMarca(); break;
                        case "5":
                            ExcluirMarca(); break;
                        case "0":
                            sair = true;
                            Console.WriteLine("Encerrando o programa..."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // ===== MÉTODOS AUXILIARES =====

        private static void VoltarParaMenu()
        {
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }

        private static string LerTexto(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string texto = (Console.ReadLine() ?? "").Trim();

                if (!string.IsNullOrWhiteSpace(texto))
                    return texto;

                Console.WriteLine("O campo não pode ficar vazio.\n");
            }
        }

        private static int LerInteiro(string mensagem, int minimo = 0)
        {
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine() ?? "";

                if (int.TryParse(entrada, out int valor) && valor >= minimo)
                    return valor;

                Console.WriteLine(
                    $"Digite um número inteiro válido, maior ou igual a {minimo}.\n"
                );
            }
        }

        private static Marca? BuscarPorIdValidado()
        {
            int id = LerInteiro("Digite o ID da marca: ", 1);
            Marca marca = Marca.ListarPorId(id);

            if (marca == null)
                Console.WriteLine($"\nMarca com ID {id} não encontrado.");

            return marca;
        }

        private static void ExibirMarca(Marca marca)
        {
            Console.WriteLine($"\nID: {marca.Id}");
            Console.WriteLine($"Nome: {marca.Nome}");
            Console.WriteLine($"Telefone: {marca.Telefone}");
            Console.WriteLine("-----------------------------");
        }

        // ===== CADASTRAR =====

        private static void CadastrarMarca()
        {
            Console.WriteLine("-=-=-=-=-=-=- CADASTRAR MARCA -=-=-=-=-=-=-\n");

            string nomeMarca = LerTexto("Digite o nome da Marca: ");
            string telefoneMarca = LerTexto("Digite o telefone da Marca: ");


            Marca novaMarca = new Marca(
                nome: nomeMarca,
                telefone: telefoneMarca
            );

            int resultado = Marca.Inserir(novaMarca);

            if (resultado > 0)
                Console.WriteLine("\nMarca cadastrado com sucesso!");
            else
                Console.WriteLine("\nNão foi possível cadastrar a Marca.");

            VoltarParaMenu();
        }

        // ===== LISTAR =====

        private static void ListarMarca()
        {
            Console.WriteLine("-=-=-=-=-=-=- LISTA DE MARCA -=-=-=-=-=-=-\n");

            List<Marca> lista = Marca.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma marca cadastrado no sistema.");
            }
            else
            {
                foreach (Marca marca in lista)
                {
                    ExibirMarca(marca);
                    Console.WriteLine();
                }
            }

            VoltarParaMenu();
        }

        // ===== BUSCAR POR ID =====

        private static void BuscarMarcaPorId()
        {
            Console.WriteLine("-=-=-=-=-=-=- BUSCAR MARCA POR ID -=-=-=-=-=-=-\n");

            Marca? marca = BuscarPorIdValidado();

            if (marca != null)
                ExibirMarca(marca);

            VoltarParaMenu();
        }

        // ===== ATUALIZAR =====

        private static void AtualizarMarca()
        {
            Console.WriteLine("-=-=-=-=-=-=- ATUALIZAR MARCA -=-=-=-=-=-=-\n");

            Marca? marca = BuscarPorIdValidado();

            if (marca == null)
            {
                VoltarParaMenu();
                return;
            }

            bool finalizar = false;

            while (!finalizar)
            {
                Console.Clear();

                Console.WriteLine("-=-=-=-=-=-=- ATUALIZAR MARCA -=-=-=-=-=-=-");
                Console.WriteLine("\nEstado atual:");
                ExibirMarca(marca);

                Console.WriteLine("\n1 - Alterar o nome");
                Console.WriteLine("2 - Alterar o telefone");
                Console.WriteLine("3 - Salvar alterações");
                Console.WriteLine("0 - Cancelar");
                Console.Write("Escolha uma opção: ");


                Console.Write("Digite uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        marca.Nome = LerTexto("Digite o novo nome: ");
                        break;

                    case "2":
                        marca.Telefone = LerTexto("Digite o novo telefone: ");
                        break;

                    case "3":
                        int resultado = Marca.AtualizarMarca(marca);

                        if (resultado > 0)
                            Console.WriteLine("\nMarca atualizado com sucesso!");
                        else
                            Console.WriteLine("\nNão foi possível atualizar o Marca.");

                        finalizar = true;
                        break;

                    case "0":
                        Console.WriteLine("\nAlterações canceladas.");
                        finalizar = true;
                        break;

                    default:
                        Console.WriteLine("\nOpção inválida.");
                        Thread.Sleep(1500);
                        break;
                }
            }

            VoltarParaMenu();
        }

        // ===== EXCLUIR =====

        private static void ExcluirMarca()
        {
            Console.WriteLine("-=-=-=-=-=-=- EXCLUIR MARCA -=-=-=-=-=-=-\n");

            Marca? marca = BuscarPorIdValidado();

            if (marca == null)
            {
                VoltarParaMenu();
                return;
            }

            ExibirMarca(marca);

            Console.Write("\nDeseja realmente excluir esta marca? (S/N): ");
            string confirmacao = (Console.ReadLine() ?? "").Trim();

            if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                int resultado = Marca.Deletar(marca);

                if (resultado > 0)
                    Console.WriteLine("\nMarca excluído com sucesso!");
                else
                    Console.WriteLine("\nNão foi possível excluir a marca.");
            }
            else
            {
                Console.WriteLine("\nExclusão cancelada.");
            }

            VoltarParaMenu();
        }
    }
}
