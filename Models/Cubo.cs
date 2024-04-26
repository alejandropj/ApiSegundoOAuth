using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSegundoOAuth.Models
{
    [Table("CUBOS")]
    public class Cubo
    {
        [Key]
        [Column("id_cubo")]
        public int id_cubo { get; set; }
        [Column("nombre")]
        public string nombre { get; set; }
        [Column("marca")]
        public string marca { get; set; }
        [Column("imagen")]
        public string imagen { get; set; }
        [Column("precio")]
        public int precio { get; set; }
    }
}
