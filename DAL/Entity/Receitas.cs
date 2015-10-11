using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    [Table("Receita")]
    public class Receita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdReceita")]
        public int IdReceita { get; set; }

        [Column("Nome")]
        [StringLength(50)]
        [Required]
        public string Nome { get; set; }

        [Column("DataRecebimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime DataRecebimento { get; set; }

        [Column("Valor")]
        [Required]
        public double Valor { get; set; }

        [Column("IdUsuarioFK")]
        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }
    }
}
