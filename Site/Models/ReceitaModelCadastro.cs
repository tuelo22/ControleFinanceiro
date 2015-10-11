using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class ReceitaModelCadastro
    {
        [Display(Name = "Nome da Receita")]
        [Required(ErrorMessage = "Por favor, Informe o nome.")]
        [RegularExpression("^[a-zA-ZÀ-Üà-ü\\s ]{6,50}$", ErrorMessage = "Erro. Nome Invalido.")]
        public string Nome { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Recebimento")]
        [Required(ErrorMessage = "Por favor, Informe a Data de Recebimento.")]
        public DateTime DataRecebimento { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Display(Name = "Valor da Receita")]
        [Required(ErrorMessage = "Por favor, Informe o Valor.")]
        public double Valor { get; set; } 
    }
}