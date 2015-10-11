using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
using System.Data.Entity;

namespace DAL.Persistence
{
    public class ReceitaDal
    {
        public void Insert(Receita r)
        {
            try
            {
                using (Conexao Con = new Conexao()) 
                {
                    Con.Receitas.Add(r);
                    Con.SaveChanges();
                }
            }
            catch
            {
                
                throw;
            }
        
        }

        public Receita FindById(int IdReceita)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    return Con.Receitas.Find(IdReceita);
                }

            }

            catch
            {
                throw;
            }


        }

        public List<Receita> FindByUsuario(int IdUsuario)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    return Con.Receitas.Where(r => r.Usuario.IdUsuario.Equals(IdUsuario)).ToList(); ;
                }

            }

            catch
            {
                throw;
            }


        }

        public void Delete(int IdReceita) 
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    Receita r = Con.Receitas.Find(IdReceita);
                    Con.Receitas.Remove(r);
                    Con.SaveChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        
        }

        public void Update(Receita r)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {

                    Con.Entry(r).State = EntityState.Modified;
                    Con.SaveChanges();
                }

            }
            catch
            {

                throw;
            }
        }

        public List<Receita> FindByRelatorio(DateTime DataInicio, DateTime DataFim, int IdUsuario)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    if (   DataInicio != DateTime.MinValue
                        && DataFim != DateTime.MinValue)
                    {
                        return Con.Receitas.Where(r => r.DataRecebimento >= DataInicio 
                                                    && r.DataRecebimento <= DataFim 
                                                    && r.Usuario.IdUsuario.Equals(IdUsuario)).ToList(); 
                    }
                    else if(DataFim != DateTime.MinValue)
                    {
                        return Con.Receitas.Where(r => r.DataRecebimento <= DataFim
                                                    && r.Usuario.IdUsuario.Equals(IdUsuario)).ToList(); 
                    }
                    else if (DataInicio != DateTime.MinValue)
                    {
                        return Con.Receitas.Where(r => r.DataRecebimento >= DataInicio
                                                    && r.Usuario.IdUsuario.Equals(IdUsuario)).ToList();
                    }
                    else {
                        return FindByUsuario(IdUsuario); 
                    }
                    
                }

            }

            catch
            {
                throw;
            }


        }
    }
}
