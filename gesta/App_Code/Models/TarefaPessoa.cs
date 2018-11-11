using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gesta.Models
{
    /// <summary>
    /// Summary description for TarefaPessoa
    /// </summary>
    [TableName("TarefaPessoa")]
    [PrimaryKey("IdTarefaPessoa", AutoIncrement = true)]
    [ExplicitColumns]
    public class TarefaPessoa
    {
        [Column("IdTarefaPessoa")]
        public int IdTarefaPessoa { get; set; }

        [Column("IdTarefa")]
        public int IdTarefa { get; set; }

        [Column("IdPessoa")]
        public int IdPessoa { get; set; }

        [ResultColumn]
        public Tarefa Tarefa
        {
            get
            {
                Database db = new Database();
                return db.FirstOrDefault<Tarefa>($"WHERE IdTarefa = {IdTarefa}");
            }
            set
            {
                IdTarefa = value.IdTarefa;
            }
        }

        [ResultColumn]
        public Pessoa Pessoa {
            get
            {
                Database db = new Database();
                return db.FirstOrDefault<Pessoa>($"WHERE IdPessoa = {IdPessoa}");
            }
            set
            {
                IdPessoa = value.IdPessoa;
            }
        }
    }
}