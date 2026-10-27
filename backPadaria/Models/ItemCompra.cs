using backPadaria.banco;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Models;

internal class ItemCompra
{
    private int _id;
    private int _compraId;
    private int _produtoId;
    private int _quantidade;
    private decimal _precoCobrado;

    public int Id { get => _id; set => _id = value; }
    public int CompraId { get => _compraId; set => _compraId = value; }
    public int ProdutoId { get => _produtoId; set => _produtoId = value; }
    public int Quantidade { get => _quantidade; set => _quantidade = value; }
    public decimal PrecoCobrado { get => _precoCobrado; set => _precoCobrado = value; }

    // Construtor base para validação e criação de um novo item em memória
    public ItemCompra(int produtoId, int quantidade, decimal precoCobrado)
    {
        if(produtoId <= 0) throw new ArgumentOutOfRangeException("O ID deve ser maior que 0");
        if(quantidade <= 0) throw new ArgumentOutOfRangeException("A quantidade deve ser maior que 0");
        if(precoCobrado <= 0) throw new ArgumentOutOfRangeException("O preço cobrado deve ser maior que 0");
        
        this.ProdutoId = produtoId;
        this.Quantidade = quantidade;
        this.PrecoCobrado = precoCobrado;
    }

    // Construtor completo (chamado ao ler da base de dados)
    public ItemCompra(int id, int compraId, int produtoId, int quantidade, decimal precoCobrado)
        :this(produtoId, quantidade, precoCobrado)
    {
        if(id <= 0) throw new ArgumentOutOfRangeException("Id do item é inválido");
        if(compraId <= 0) throw new ArgumentOutOfRangeException("Id da compra é inválido");


        this.Id = id;
        this.CompraId = compraId;
    }

    public decimal CalcularSubTotal()
    {
        return this.Quantidade * this.PrecoCobrado;
    }

    public int Inserir(ItemCompra item)
    {
        using (var cmd = new MySqlCommand("sp_InserirItemCompra"))
        {
            // Configura o comando como storedProcedure
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_CompraID", item.CompraId);
            cmd.Parameters.AddWithValue("p_ProdutoID", item.ProdutoId);
            cmd.Parameters.AddWithValue("p_Quantidade", item.Quantidade);
            cmd.Parameters.AddWithValue("p_PrecoCobrado", item.PrecoCobrado);

            return Banco.ExecutarComando(cmd);
        }
    }
}
