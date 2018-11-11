using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gesta.Models;
using PetaPoco;


namespace gesta.Controllers
{
    /// <summary>
    /// Summary description for MateriaController
    /// </summary>
    public class MateriaController
    {
        public MateriaController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static Materia Inseir(Materia materia)
        {
            Database db = new Database();
            int idMateria = (int)db.Insert(materia);
            materia.IdMateria = idMateria;
            return materia;
        }

        public static void Editar(Materia Materia)
        {
            Database db = new Database();
            db.Update(Materia);
        }

        public static void Remover(Materia Materia)
        {
            Database db = new Database();
            db.Delete(Materia);
        }

        public static List<Materia> Lista()
        {
            Database db = new Database();
            return db.Fetch<Materia>("WHERE 1=1");
        }
    }
}