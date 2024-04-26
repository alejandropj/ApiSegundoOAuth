using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSegundoOAuth.Models
{
    [Table("COMPRACUBOS")]
    public class CompraCubos
    {
        [Key]
        [Column("id_pedido")]
        public int id_pedido { get; set; }
        [Column("id_cubo")]
        public int id_cubo { get; set; }
        [Column("id_usuario")]
        public int id_usuario { get; set; }
        [Column("fechapedido")]
        public DateTime fechapedido { get; set; }
    }
}
