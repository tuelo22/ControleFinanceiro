using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    [Table("ContaPagar")]
    public class ContaPagar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdContaPagar")]
        public int IdContaPagar { get; set; }

        [Column("Nome")]
        [StringLength(50)]
        [Required]
        public string Nome { get; set; }

        [Column("DataPagamento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime DataPagamento { get; set; }

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
