using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class ContaPagarModelAtualizar
    {
        [Required(ErrorMessage = "Por favor, selecione uma Conta a Pagar.")]
        public int IdContaPagar { get; set; }

        [Display(Name = "Nome da Conta a Pagar")]
        [Required(ErrorMessage = "Por favor, Informe o nome.")]
        [RegularExpression("^[a-zA-ZÀ-Üà-ü\\s ]{6,50}$", ErrorMessage = "Erro. Nome Invalido.")]
        public string Nome { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Pagamento")]
        [Required(ErrorMessage = "Por favor, Informe a Data de Pagamento.")]
        public DateTime DataPagamento { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Display(Name = "Valor da Conta a Pagar")]
        [Required(ErrorMessage = "Por favor, Informe o Valor.")]
        public double Valor { get; set; }
    }
}