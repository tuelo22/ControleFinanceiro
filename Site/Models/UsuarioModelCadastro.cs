using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class UsuarioModelCadastro
    {
        [Required(ErrorMessage = "Por favor, informe o nome do usuário.")]
        [RegularExpression("^[a-zA-ZÀ-Üà-ü\\s]{6,50}$", ErrorMessage = "Erro. Nome Invalido.")]
        [Display(Name="Nome do Usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o login do usuário.")]
        [RegularExpression("^[a-z0-9_]{6,50}$", ErrorMessage = "Erro. Login Invalido.")]
        [Display(Name = "Nome do Usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha do usuario.")]
        [RegularExpression("^[a-zA-Z0-9]{6,10}$", ErrorMessage = "Erro. Senha inválida.")]
        [Display(Name = "Senha de Acesso:")] 
        public string Senha { get; set; }

        [Required(ErrorMessage = "Por favor, confirme a sneha do usuário.")]
        [RegularExpression("^[a-zA-Z0-9]{6,10}$", ErrorMessage = "Erro. Confirmação de senha inválida.")]
        [Display(Name = "Confirme sua Senha")] 
        public string SenhaConfirm { get; set; }
    }
}