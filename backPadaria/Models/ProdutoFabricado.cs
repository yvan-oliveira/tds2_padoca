using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Models;

internal class ProdutoFabricado : Produto
{
    private int _produtoId;
    private string _descricaoProducao;

    public ProdutoFabricado(
        string nome,
        decimal preco,
        string categoria, 
        bool fabricado, 
        int marcaId,
        int produtoId,
        string descricaoProducao)
        : base(nome, preco, categoria, true, marcaId)
    {
        this._produtoId = produtoId;
        this._descricaoProducao = descricaoProducao;

    }
}
