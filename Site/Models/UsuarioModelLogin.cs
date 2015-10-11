using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class UsuarioModelLogin
    {
        [Required(ErrorMessage = "Por favor, informe seu Login.")]
        [RegularExpression("^[a-z0-9_]{6,20}$", ErrorMessage = "Erro. Login Inválido.")]
        [Display(Name = "Login de Acesso")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Por favor, informe sua Senha.")]
        [RegularExpression("^[a-zA-Z0-9]{6,20}$", ErrorMessage = "Erro. Senha Inválida.")]
        [Display(Name = "Senha de Acesso")]
        public string Senha { get; set; }
    }
}