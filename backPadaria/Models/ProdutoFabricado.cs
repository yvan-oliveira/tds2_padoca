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

    public ProdutoFabricado(int id, string nome, string preco, string categoria, bool fabricado, int marcaId, string descricao) : base(id, nome, preco, categoria, fabricado, marcaId)
    {
        this._produtoId = id;
        this._descricaoProducao = descricao;

    }
}
