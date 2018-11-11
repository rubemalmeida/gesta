using PetaPoco;

namespace gesta.Models
{
    [TableName("Pessoa")]
    [PrimaryKey("IdPessoa", AutoIncrement = true)]
    public class Pessoa
    {
        [Column("IdPessoa")]
        public int IdPessoa { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Papel")]
        public Papel Papel { get; set; }
    }
}

