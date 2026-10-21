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
    private string _preco;
    private string _categoria;
    private bool _fabricado;
    private int _marcaId;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public string Preco { get => _preco; set => _preco = value; }
    public string Categoria { get => _categoria; set => _categoria = value; }
    public bool Fabricado { get => _fabricado; set => _fabricado = value; }
    public int MarcaId { get => _marcaId; set => _marcaId = value; }

    public Produto(int id, string nome, string preco, string categoria, bool fabricado, int marcaId)
    {
        this._id = id;
        this._nome = nome;
        this._preco = preco;
        this._categoria = categoria;
        this._fabricado = fabricado;
        this._marcaId = marcaId;
    }



}
