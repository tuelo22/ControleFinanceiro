using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        
        [Column("Nome")][StringLength(50)][Required]
        public string Nome { get; set; }

        [Column("Login")][StringLength(20)][Required][Index("idxLogin", IsUnique = true)]
        public string Login { get; set; }

        [Column("Senha")][StringLength(50)][Required]
        public string Senha { get; set; }

        public virtual List<Receita> Receitas { get; set; }
    }
}
