using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
using System.Data.Entity;

namespace DAL.Persistence
{
    public class UsuarioDal
    {
        public void Inserir(Usuario u)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    Con.Usuarios.Add(u);
                    Con.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir o Usuário: " + ex.Message);
            }
        }

        public bool HasLogin(string Login)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    return Con.Usuarios.Where(u => u.Login.Equals(Login)).Count() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter Login do Usuário: " + ex.Message);
            }
        
        }

        public Usuario Find(string Login, string Senha)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    return Con.Usuarios.Where(u => u.Login.Equals(Login) && u.Senha.Equals(Senha)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter Usuário: " + ex.Message);
            }
        }
    }
}
