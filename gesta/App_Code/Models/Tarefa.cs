using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
using gesta;

namespace gesta.Models
{
    [TableName("Tarefa")]
    [PrimaryKey("IdTarefa", AutoIncrement = true)]
    [ExplicitColumns]
    public class Tarefa
    {
        [Column("IdTarefa")]
        public int IdTarefa { get; set; }

        [Column("DescricaoResumida")]
        public string DescricaoResumida { get; set; }

        [Column("DescricaoCompleta")]
        public string DescricaoCompleta { get; set; }

        [Column("TipoTarefa")]
        public TipoTarefa TipoTarefa { get; set; }

        [Column("DataCadastro")]
        public DateTime DataCadastro { get; set; }

        [Column("DataLimite")]
        public DateTime DataLimite { get; set; }

        [ResultColumn]
        public List<Pessoa> Participantes { get; set; }

        [ResultColumn]
        public List<Materia> Materias { get; set; }

        [ResultColumn]
        public List<Pessoa> ParticipantesRemoto
        {
            get
            {
                Database db = new Database();
                var listaTarefaPessoa = db.Fetch<TarefaPessoa>($"WHERE IdTarefa = {IdTarefa}");
                var listaPessoas = new List<Pessoa>();
                foreach (var tarefaPessoa in listaTarefaPessoa)
                {
                    listaPessoas.Add(tarefaPessoa.Pessoa);
                }
                return listaPessoas;
            }
        }

        [ResultColumn]
        public List<Materia> MateriasRemoto
        {
            get
            {
                Database db = new Database();
                var listaTarefaMateria = db.Fetch<TarefaMateria>($"WHERE IdTarefa = {IdTarefa}");
                var listaMaterias = new List<Materia>();
                foreach (var tarefaMateria in listaTarefaMateria)
                {
                    listaMaterias.Add(tarefaMateria.Materia);
                }
                return listaMaterias;
            }
        }
    }
}
