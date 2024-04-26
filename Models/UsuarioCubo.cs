using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSegundoOAuth.Models
{
    [Table("USUARIOSCUBO")]
    public class UsuarioCubo
    {
        [Key]
        [Column("ID_USUARIO")]
        public int id_usuario { get; set; }
        [Column("NOMBRE")]
        public string nombre { get; set; }
        [Column("EMAIL")]
        public string email { get; set; }
        [Column("PASS")]
        public string pass { get; set; }
        [Column("imagen")]
        public string imagen { get; set; }
    }
}
