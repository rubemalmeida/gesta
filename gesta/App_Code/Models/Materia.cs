using PetaPoco;

namespace gesta.Models
{
    [TableName("Materia")]
    [PrimaryKey("IdMateria", AutoIncrement = true)]
    public class Materia
    {
        [Column("IdMateria")]
        public int IdMateria { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }
    }
}
