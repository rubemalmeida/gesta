using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace gesta.Models
{
    /// <summary>
    /// Summary description for TarefaMateria
    /// </summary>
    [TableName("TarefaMateria")]
    [PrimaryKey("IdTarefaMateria", AutoIncrement = true)]
    [ExplicitColumns]
    public class TarefaMateria
    {
        [Column("IdTarefaMateria")]
        public int IdTarefaMateria { get; set; }

        [Column("IdTarefa")]
        public int IdTarefa { get; set; }

        [Column("IdMateria")]
        public int IdMateria { get; set; }
        
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
        public Materia Materia
        {
            get
            {
                Database db = new Database();
                return db.FirstOrDefault<Materia>($"WHERE IdPessoa = {IdMateria}");
            }
            set
            {
                IdMateria = value.IdMateria;
            }
        }
    }
}