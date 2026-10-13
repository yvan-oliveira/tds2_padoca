using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Models;

internal class Cliente
{
    private int _id;
    private string _nome;
    private string _cpf;
    private string _telefone;
    private string _endereco;
    private int _pontos;

    public Cliente(string nome, string cpf, string telefone, string endereco, int pontos)
    {
        _nome = nome;
        _cpf = cpf;
        _telefone = telefone;
        _endereco = endereco;
        _pontos = pontos;
    }

    public Cliente(int id, string nome, string cpf, string telefone, string endereco, int pontos)
    {
        this._id = id;
        this._nome = nome;
        this._cpf = cpf;
        this._telefone = telefone;
        this._endereco = endereco;
        this._pontos = pontos;
    }
}
