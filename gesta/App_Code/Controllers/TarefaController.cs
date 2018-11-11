using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gesta.Models;
using PetaPoco;

namespace gesta.Controllers
{
    /// <summary>
    /// Summary description for TarefaController
    /// </summary>
    /// 
    public class TarefaController
    {
        public TarefaController()
        {

        }

        public static Tarefa Inseir(Tarefa tarefa)
        {
            Database db = new Database();
            var x = db.Insert(tarefa);
            int idTarefa = (int)x;
            tarefa.IdTarefa = idTarefa;
            
            foreach (var participante in tarefa.Participantes)
            {
                db.Insert(new TarefaPessoa()
                {
                    IdTarefa = tarefa.IdTarefa,
                    IdPessoa = participante.IdPessoa
                });
            }
            foreach (var materia in tarefa.Materias)
            {
                db.Insert(new TarefaMateria()
                {
                    IdTarefa = tarefa.IdTarefa,
                    IdMateria = materia.IdMateria
                });
            }
            return tarefa;
        }

        public static void Editar(Tarefa tarefa)
        {
            Database db = new Database();
            db.Update(tarefa);

            foreach (var participante in tarefa.ParticipantesRemoto)
            {
                var tarefaPessoa = db.Fetch<TarefaPessoa>($"WHERE IdTarefa={tarefa.IdTarefa} AND IdPessoa={participante.IdPessoa}");
                db.Delete(tarefaPessoa);
            }
            foreach (var participante in tarefa.Participantes)
            {
                db.Insert(new TarefaPessoa()
                {
                    IdTarefa = tarefa.IdTarefa,
                    IdPessoa = participante.IdPessoa
                });
            }

            foreach (var materia in tarefa.MateriasRemoto)
            {
                var tarefaMateria = db.Fetch<TarefaMateria>($"WHERE IdTarefa={tarefa.IdTarefa} AND IdMateria={materia.IdMateria}");
                db.Delete(tarefaMateria);
            }
            foreach (var materia in tarefa.Materias)
            {
                db.Insert(new TarefaMateria()
                {
                    IdTarefa = tarefa.IdTarefa,
                    IdMateria = materia.IdMateria
                });
            }

            db.Update(tarefa);
        }

        public static void Remover(Tarefa tarefa)
        {
            Database db = new Database();
            db.Delete<TarefaPessoa>($"WHERE IdTarefa={tarefa.IdTarefa}");
            db.Delete<TarefaMateria>($"WHERE IdTarefa={tarefa.IdTarefa}");
            db.Delete(tarefa);
        }

        public static List<Tarefa> Lista()
        {
            Database db = new Database();
            return db.Fetch<Tarefa>("WHERE 1=1");
        }
    }
}