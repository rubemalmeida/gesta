using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gesta.Models;
using PetaPoco;


namespace gesta.Controllers
{
    /// <summary>
    /// Summary description for PessoaController
    /// </summary>
    public class PessoaController
    {
        public PessoaController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static Pessoa Inseir(Pessoa pessoa)
        {
            Database db = new Database();
            int idPessoa = (int)db.Insert(pessoa);
            pessoa.IdPessoa = idPessoa;
            return pessoa;
        }

        public static void Editar(Pessoa Pessoa)
        {
            Database db = new Database();
            db.Update(Pessoa);
        }

        public static void Remover(Pessoa Pessoa)
        {
            Database db = new Database();
            db.Delete(Pessoa);
        }

        public static List<Pessoa> Lista()
        {
            Database db = new Database();
            return db.Fetch<Pessoa>($"WHERE 1=1");
        }
    }
}